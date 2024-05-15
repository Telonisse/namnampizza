using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using static Meta.XR.MRUtilityKit.MRUK;

public class FindSpawnPos : MonoBehaviour
{
    [SerializeField] GameObject table;
    [SerializeField] GameObject fridge;
    [SerializeField] GameObject oven;
    [SerializeField] GameObject lucka;
    [SerializeField] Transform[] roomObjects;
    [SerializeField] Transform floor;

    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private MRUKRoom room;
    private int numOfWalls = 0;
    [SerializeField] Transform[] walls;
    public List<Transform> wallsList;
    private int currentWall = 0;

    //spawn counters
    private GameObject spawnedCounters = null;
    private bool countersSpawned = false;
    private int currentTable = 0;

    //spawn fridge
    private GameObject spawnedFridge = null;

    private Vector3 boxCenterFridge;
    private Vector3 boxSizeFridge;
    private float movedFridge = 0;
    private float maxMoveFridge = 0;
    private bool moveOnceFridge = false;
    private bool fridgeDone = false;
    private Vector3 prevPosFridge;
    private float lastTimeMovedFridge;

    //spawn oven
    private GameObject spawnedOven = null;

    private Vector3 boxCenterOven;
    private Vector3 boxSizeOven;
    private float movedOven = 0;
    private float maxMoveOven = 0;
    private bool moveOnceOven = false;
    private bool ovenDone = false;
    private Vector3 prevPosOven;
    private float lastTimeMovedOven;

    //Spawn lucka
    private GameObject spawnedLucka = null;

    private Vector3 boxCenterLucka;
    private Vector3 boxSizeLucka;
    private float movedLucka = 0;
    private float maxMoveLucka = 0;
    private bool moveOnceLucka = false;
    //private bool luckaDone = false;
    //private Vector3 prevPosLucka;
    //private float lastTimeMovedLucka;

    private bool roomLoaded = false;

    public void FindSpawnPosOnSurface()
    {
        room = FindObjectOfType<MRUKRoom>();

        //Get all children of room
        int childrenInRoom = room.transform.childCount;
        roomObjects = new Transform[childrenInRoom];
        for (int i = 0; i < childrenInRoom; i++)
        {
            roomObjects[i] = room.transform.GetChild(i);
            if (room.transform.GetChild(i).GetComponent<MRUKAnchor>().HasLabel("WALL_FACE"))
            {
                numOfWalls++;
                wallsList.Add(room.transform.GetChild(i));
            }
        }
        walls = wallsList.ToArray();
        //check what type of object and spawn object on it
        foreach (Transform child in roomObjects)
        {
            if (child.GetComponent<MRUKAnchor>().HasLabel("TABLE") || child.GetComponent<MRUKAnchor>().HasLabel("OTHER"))
            {
                if (countersSpawned == false)
                {
                    spawnPos = child.transform.position;
                    spawnPos.y = 0;
                    spawnRot = Quaternion.LookRotation(room.GetFacingDirection(child.GetComponent<MRUKAnchor>()));
                    spawnedCounters = Instantiate(table, spawnPos, spawnRot, transform);
                    currentTable++;
                    countersSpawned = true;
                }
            }
        }
        if (spawnedCounters == null)
        {
            spawnPos = Vector3.zero;
            spawnRot = Quaternion.identity;
            spawnedCounters = Instantiate(table, spawnPos, spawnRot, transform);
        }

        //spawn fridge and check walls until it doesnt collide with any other object
        room.GenerateRandomPositionOnSurface(SurfaceType.FACING_UP, 0.7f, LabelFilter.FromEnum(MRUKAnchor.SceneLabels.FLOOR), out Vector3 pos, out Vector3 normal);

        if (walls != null)
        {
            GameObject closestWall = null;
            float closestDistance = Mathf.Infinity;

            foreach (Transform wall in walls)
            {
                float distanceToWall = Vector3.Distance(pos, wall.transform.position);
                if (distanceToWall < closestDistance)
                {
                    closestWall = wall.gameObject;
                    closestDistance = distanceToWall;
                }
            }
            Vector3 lookPos = new Vector3(closestWall.transform.position.x, 0f, closestWall.transform.position.z);
            spawnRot = Quaternion.LookRotation(-lookPos);
            spawnedFridge = Instantiate(fridge, pos, spawnRot);
        }

        //spawn oven
        room.GenerateRandomPositionOnSurface(SurfaceType.FACING_UP, 0.7f, LabelFilter.FromEnum(MRUKAnchor.SceneLabels.FLOOR), out Vector3 pos1, out Vector3 normal1);

        if (walls != null)
        {
            GameObject closestWall = null;
            float closestDistance = Mathf.Infinity;

            foreach (Transform wall in walls)
            {
                float distanceToWall = Vector3.Distance(pos1, wall.transform.position);
                if (distanceToWall < closestDistance)
                {
                    closestWall = wall.gameObject;
                    closestDistance = distanceToWall;
                }
            }
            Vector3 lookPos = new Vector3(closestWall.transform.position.x, 0f, closestWall.transform.position.z);
            spawnRot = Quaternion.LookRotation(-lookPos);
            spawnedOven = Instantiate(oven, pos1, spawnRot);
        }

        //Spawn lucka
        spawnedLucka = Instantiate(lucka, walls[0].transform.position, walls[0].transform.rotation);

        prevPosFridge = spawnedFridge.transform.position;
        lastTimeMovedFridge = Time.time;

        prevPosOven = spawnedOven.transform.position;
        lastTimeMovedOven = Time.time;

        roomLoaded = true;
    }
    private void Update()
    {
        if (roomLoaded)
        {
            //FIND POS COUNTERS
            SpawnCounters();

            //FIND POS FRIDGE
            SpawnFridge();

            //FIND POS OVEN
            SpawnOven();

            //FIND POS LUCKA
            SpawnLucka();

            CanSpawnCounters();
        }
    }

    private void SpawnFridge()
    {
        boxCenterFridge = new Vector3(spawnedFridge.transform.position.x, spawnedFridge.transform.position.y + 0.6f, spawnedFridge.transform.position.z);
        boxSizeFridge = new Vector3(1f, 1.2f, 1f);
        Collider[] collidersFridge = Physics.OverlapBox(boxCenterFridge, boxSizeFridge / 2f, spawnedFridge.transform.rotation);

        for (int i = 0; i < collidersFridge.Length; i++)
        {
            MRUKAnchor anchor = collidersFridge[i].GetComponentInParent<MRUKAnchor>();
            if (anchor != null)
            {
                if (anchor.HasLabel("TABLE") || anchor.HasLabel("OTHER") || anchor.HasLabel("COUCH"))
                {
                    Physics.ComputePenetration(spawnedFridge.GetComponent<Collider>(), spawnedFridge.transform.position, spawnedFridge.transform.rotation, collidersFridge[i], collidersFridge[i].transform.position, collidersFridge[i].transform.rotation, out Vector3 dir, out float dis);
                    spawnedFridge.transform.position += (dir * dis);
                    if (walls != null)
                    {
                        GameObject closestWall = null;
                        float closestDistance = Mathf.Infinity;

                        foreach (Transform wall in walls)
                        {
                            float distanceToWall = Vector3.Distance(spawnedFridge.transform.position, wall.transform.position);
                            if (distanceToWall < closestDistance)
                            {
                                closestWall = wall.gameObject;
                                closestDistance = distanceToWall;
                            }
                        }
                        Vector3 lookPos = new Vector3(closestWall.transform.position.x, 0f, closestWall.transform.position.z);
                        spawnedFridge.transform.rotation = Quaternion.LookRotation(-lookPos);
                    }
                }
            }
            spawnedFridge.GetComponent<Collider>().enabled = false;
        }
    }

    private void SpawnLucka()
    {
        boxCenterLucka = new Vector3(spawnedLucka.transform.position.x, spawnedLucka.transform.position.y, spawnedLucka.transform.position.z);
        boxSizeLucka = new Vector3(0.5f, 0.5f, 0.5f);
        Collider[] collidersLucka = Physics.OverlapBox(boxCenterLucka, boxSizeLucka / 2f, spawnedLucka.transform.rotation);

        for (int i = 0; i < collidersLucka.Length; i++)
        {
            MRUKAnchor anchor = collidersLucka[i].GetComponent<MRUKAnchor>();
            if (anchor != null)
            {
                if (anchor.HasLabel("TABLE") || anchor.HasLabel("OTHER") || anchor.HasLabel("COUCH"))
                {
                    currentWall++;
                    spawnedLucka.transform.position = walls[currentWall].transform.position;
                    spawnedLucka.transform.rotation = walls[currentWall].transform.rotation;
                }
            }
            if (collidersLucka[i].name == "Fridge" || collidersLucka[i].name == "Oven")
            {
                currentWall++;
                spawnedLucka.transform.position = walls[currentWall].transform.position;
                spawnedLucka.transform.rotation = walls[currentWall].transform.rotation;
            }
        }
    }

    private void SpawnOven()
    {
        boxCenterOven = new Vector3(spawnedOven.transform.position.x, spawnedOven.transform.position.y + 0.6f, spawnedOven.transform.position.z);
        boxSizeOven = new Vector3(1.2f, 1.2f, 1.2f);
        Collider[] collidersOven = Physics.OverlapBox(boxCenterOven, boxSizeOven / 2f, spawnedOven.transform.rotation);

        for (int i = 0; i < collidersOven.Length; i++)
        {
            MRUKAnchor anchor = collidersOven[i].GetComponent<MRUKAnchor>();
            if (anchor != null)
            {
                if (anchor.HasLabel("TABLE") || anchor.HasLabel("OTHER") || anchor.HasLabel("COUCH"))
                {
                    Physics.ComputePenetration(spawnedOven.GetComponent<Collider>(), spawnedOven.transform.position, spawnedOven.transform.rotation, collidersOven[i], collidersOven[i].transform.position, collidersOven[i].transform.rotation, out Vector3 dir, out float dis);
                    spawnedOven.transform.position += (dir * dis);
                    if (walls != null)
                    {
                        GameObject closestWall = null;
                        float closestDistance = Mathf.Infinity;

                        foreach (Transform wall in walls)
                        {
                            float distanceToWall = Vector3.Distance(spawnedOven.transform.position, wall.transform.position);
                            if (distanceToWall < closestDistance)
                            {
                                closestWall = wall.gameObject;
                                closestDistance = distanceToWall;
                            }
                        }
                        Vector3 lookPos = new Vector3(closestWall.transform.position.x, 0f, closestWall.transform.position.z);
                        spawnedOven.transform.rotation = Quaternion.LookRotation(-lookPos);

                    }
                }
            }
            if (collidersOven[i].name == "Fridge(Clone)")
            {

                Physics.ComputePenetration(spawnedOven.GetComponent<Collider>(), spawnedOven.transform.position, spawnedOven.transform.rotation, collidersOven[i], collidersOven[i].transform.position, collidersOven[i].transform.rotation, out Vector3 dir, out float dis);
                spawnedOven.transform.position += (dir * dis);
                if (walls != null)
                {
                    GameObject closestWall = null;
                    float closestDistance = Mathf.Infinity;

                    foreach (Transform wall in walls)
                    {
                        float distanceToWall = Vector3.Distance(spawnedOven.transform.position, wall.transform.position);
                        if (distanceToWall < closestDistance)
                        {
                            closestWall = wall.gameObject;
                            closestDistance = distanceToWall;
                        }
                    }
                    Vector3 lookPos = new Vector3(closestWall.transform.position.x, 0f, closestWall.transform.position.z);
                    spawnedOven.transform.rotation = Quaternion.LookRotation(-lookPos);
                }
            }
            else
            {
                spawnedOven.GetComponent<BoxCollider>().enabled = false;

            }
        }

    }

    private void SpawnCounters()
    {
        BoxCollider boxcoll = spawnedCounters.GetComponentInChildren<BoxCollider>();
        Vector3 vector3 = new Vector3(boxcoll.transform.position.x, boxcoll.transform.position.y + 0.6f, boxcoll.transform.position.z);
        Collider[] colliders = Physics.OverlapBox(vector3, boxcoll.size / 2f, spawnedCounters.transform.rotation);
        int table = 0;
        if (ovenDone == true && fridgeDone == true)
        {
            spawnedCounters.GetComponent<Counters>().SetNonKinematic();
            foreach (Collider collider in colliders)
            {
                if (collider.transform.name == "Fridge" || collider.transform.name == "Furnace")
                {
                    foreach (Transform trans in roomObjects)
                    {
                        if (trans.GetComponent<MRUKAnchor>().HasLabel("TABLE") || trans.GetComponent<MRUKAnchor>().HasLabel("OTHER"))
                        {
                            table++;
                            if (table == currentTable + 1)
                            {
                                spawnedCounters.transform.position = new Vector3(trans.transform.position.x, 0, trans.transform.position.z);
                                spawnedCounters.transform.rotation = Quaternion.LookRotation(room.GetFacingDirection(trans.GetComponent<MRUKAnchor>()));
                            }
                        }
                    }
                }
            }
        }
    }

    private void CanSpawnCounters()
    {
        if (spawnedFridge.transform.position != prevPosFridge)
        {
            prevPosFridge = spawnedFridge.transform.position;
            lastTimeMovedFridge = Time.time;
        }
        else if (Time.time - lastTimeMovedFridge > 1f)
        {
            fridgeDone = true;
            spawnedFridge.GetComponentInChildren<Fridge>().MovedDone();
        }
        if (spawnedOven.transform.position != prevPosOven)
        {
            prevPosOven = spawnedOven.transform.position;
            lastTimeMovedOven = Time.time;
        }
        else if (Time.time - lastTimeMovedOven > 1f)
        {
            ovenDone = true;
        }
    }

}
