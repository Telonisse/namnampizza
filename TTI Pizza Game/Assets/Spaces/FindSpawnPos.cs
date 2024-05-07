using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private int currentWall = 1;

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
        maxMoveFridge = walls[0].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
        spawnPos.x = walls[0].transform.position.x;
        spawnPos.y = walls[0].transform.position.y;
        spawnPos.z = walls[0].transform.position.z;
        spawnRot = walls[0].transform.rotation;
        spawnedFridge = Instantiate(fridge, spawnPos, spawnRot);
        if (!moveOnceFridge)
        {
            spawnedFridge.transform.Translate(-maxMoveFridge, 0, 0.5f, Space.Self);
            moveOnceFridge = true;
        }
        //spawn oven
        maxMoveOven = walls[0].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
        spawnedOven = Instantiate(oven, spawnPos, spawnRot);
        if (!moveOnceOven)
        {
            spawnedOven.transform.Translate(-maxMoveOven + 0.2f, 0, 0.5f, Space.Self);
            moveOnceOven = true;
        }

        //Spawn lucka
        maxMoveLucka = walls[0].gameObject.GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
        spawnedLucka = Instantiate(lucka, spawnPos, spawnRot);
        if (!moveOnceLucka)
        {
            spawnedLucka.transform.Translate(-maxMoveLucka, 1.2f, 0.5f, Space.Self);
            moveOnceLucka = true;
        }

        prevPosFridge = spawnedFridge.transform.position;
        lastTimeMovedFridge = Time.time;

        prevPosOven = spawnedOven.transform.position;
        lastTimeMovedOven = Time.time;
    }
    private void Update()
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

    private void SpawnLucka()
    {
        boxCenterLucka = new Vector3(spawnedLucka.transform.position.x, spawnedLucka.transform.position.y, spawnedLucka.transform.position.z);
        boxSizeLucka = new Vector3(0.5f, 0.5f, 0.5f);
        Collider[] collidersLucka = Physics.OverlapBox(boxCenterLucka, boxSizeLucka / 2f, spawnedLucka.transform.rotation);

        spawnedLucka.transform.position = new Vector3(spawnedLucka.transform.position.x, 1.3f, spawnedLucka.transform.position.z);

        if (collidersLucka.Length > 0)
        {
            for (int i = 0; i < collidersLucka.Length; i++)
            {
                MRUKAnchor anchor = collidersLucka[i].GetComponentInParent<MRUKAnchor>();
                if (anchor != null)
                {
                    maxMoveLucka = walls[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                }
                if (anchor != null && !anchor.HasLabel("WALL_FACE"))
                {
                    spawnedLucka.transform.Translate(0.01f, 0, 0, Space.Self);
                    movedLucka += 0.01f;
                }
                if (collidersLucka[i].tag == "Fridge")
                {
                    spawnedLucka.transform.Translate(0.01f, 0, 0, Space.Self);
                    movedLucka += 0.01f;
                }
                if (anchor != null && maxMoveLucka < movedLucka)
                {
                    movedLucka = 0;
                    currentWall++;
                    if (currentWall > walls.Length)
                    {
                        currentWall = 1;
                    }
                    movedLucka = 0;
                    moveOnceLucka = false;
                    maxMoveLucka = walls[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                    spawnedLucka.transform.position = new Vector3(walls[currentWall].transform.position.x, walls[currentWall].transform.position.y, walls[currentWall].transform.position.z);
                    spawnedLucka.transform.rotation = walls[currentWall].transform.rotation * Quaternion.Euler(0, 0, 0);
                    if (!moveOnceLucka)
                    {
                        spawnedLucka.transform.Translate(-maxMoveLucka + 0.5f, 1.2f, 0.5f, Space.Self);
                        moveOnceLucka = true;
                    }
                }
            }
        }
    }

    private void SpawnOven()
    {
        boxCenterOven = new Vector3(spawnedOven.transform.position.x, spawnedOven.transform.position.y + 0.6f, spawnedOven.transform.position.z);
        boxSizeOven = new Vector3(0.8f, 1.2f, 0.8f);
        Collider[] collidersOven = Physics.OverlapBox(boxCenterOven, boxSizeOven / 2f, spawnedOven.transform.rotation);

        spawnedOven.transform.position = new Vector3(spawnedOven.transform.position.x, 0 + 0.0001f, spawnedOven.transform.position.z);

        if (collidersOven.Length > 0)
        {
            for (int i = 0; i < collidersOven.Length; i++)
            {
                MRUKAnchor anchor = collidersOven[i].GetComponentInParent<MRUKAnchor>();
                if (anchor != null)
                {
                    maxMoveOven = walls[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                }
                if (anchor != null && !anchor.HasLabel("WALL_FACE"))
                {
                    spawnedOven.transform.Translate(0.01f, 0, 0, Space.Self);
                    movedOven += 0.01f;
                }
                if (collidersOven[i].tag == "Fridge")
                {
                    spawnedOven.transform.Translate(0.01f, 0, 0, Space.Self);
                    movedOven += 0.01f;
                }
                if (anchor != null && maxMoveOven < movedOven)
                {
                    movedOven = 0;
                    currentWall++;
                    if (currentWall > walls.Length)
                    {
                        currentWall = 0;
                    }
                    movedOven = 0;
                    moveOnceOven = false;
                    maxMoveOven = walls[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                    spawnedOven.transform.position = new Vector3(walls[currentWall].transform.position.x, walls[currentWall].transform.position.y, walls[currentWall].transform.position.z);
                    spawnedOven.transform.rotation = walls[currentWall].transform.rotation * Quaternion.Euler(0, 0, 0);
                    if (!moveOnceOven)
                    {
                        spawnedOven.transform.Translate(-maxMoveOven + 0.5f, 0, 0.5f, Space.Self);
                        moveOnceOven = true;
                    }
                }
            }
        }
    }

    private void SpawnFridge()
    {
        boxCenterFridge = new Vector3(spawnedFridge.transform.position.x, spawnedFridge.transform.position.y + 0.6f, spawnedFridge.transform.position.z);
        boxSizeFridge = new Vector3(0.8f, 1.2f, 0.8f);
        Collider[] collidersFridge = Physics.OverlapBox(boxCenterFridge, boxSizeFridge / 2f, spawnedFridge.transform.rotation);

        spawnedFridge.transform.position = new Vector3(spawnedFridge.transform.position.x, 0, spawnedFridge.transform.position.z);

        if (collidersFridge.Length > 0)
        {
            for (int i = 0; i < collidersFridge.Length; i++)
            {
                MRUKAnchor anchor = collidersFridge[i].GetComponentInParent<MRUKAnchor>();
                if (anchor != null && anchor.HasLabel("WALL_FACE"))
                {
                    fridgeDone = true;
                }
                if (anchor != null)
                {
                    maxMoveFridge = walls[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                }
                if (anchor != null && !anchor.HasLabel("WALL_FACE"))
                {
                    spawnedFridge.transform.Translate(0.01f, 0, 0, Space.Self);
                    movedFridge += 0.01f;
                }
                if (anchor != null && maxMoveFridge < movedFridge)
                {
                    movedFridge = 0;
                    currentWall++;
                    if (currentWall > walls.Length)
                    {
                        currentWall = 1;
                    }
                    movedFridge = 0;
                    moveOnceFridge = false;
                    maxMoveFridge = walls[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                    spawnedFridge.transform.position = new Vector3(walls[currentWall].transform.position.x, walls[currentWall].transform.position.y, walls[currentWall].transform.position.z);
                    spawnedFridge.transform.rotation = walls[currentWall].transform.rotation * Quaternion.Euler(0, 0, 0);
                    if (!moveOnceFridge)
                    {
                        spawnedFridge.transform.Translate(-maxMoveFridge + 0.5f, 0, 0.5f, Space.Self);
                        moveOnceFridge = true;
                    }
                }
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
