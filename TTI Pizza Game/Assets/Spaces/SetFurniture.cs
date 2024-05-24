using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFurniture : MonoBehaviour
{
    [SerializeField] GameObject fridge;
    [SerializeField] GameObject oven;
    [SerializeField] GameObject lucka;
    [SerializeField] GameObject table;
    [SerializeField] GameObject door;

    public GameController gameController;
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
    public GameObject spawnedCounters = null;
    private bool countersSpawned = false;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void SetFurniturePlace()
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
                    //spawnedCounters = Instantiate(table, spawnPos, spawnRot, transform);
                    table.transform.position = spawnPos;
                    table.transform.rotation = spawnRot;
                    countersSpawned = true;
                }
            }
        }
        //if (spawnedCounters == null)
        //{
        //    spawnPos = Vector3.zero;
        //    spawnRot = Quaternion.identity;
        //    spawnedCounters = Instantiate(table, spawnPos, spawnRot, transform);
        //}

        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }
        gameController.GetPos(out Vector3 posFridge, out Vector3 posOven, out Vector3 posLucka, out Vector3 posDoor);
        if (fridge != null)
        {
            fridge.transform.position = posFridge;
            if (walls != null)
            {
                GameObject closestWall = null;
                float closestDistance = Mathf.Infinity;

                foreach (Transform wall in walls)
                {
                    float distanceToWall = Vector3.Distance(fridge.transform.position, wall.transform.position);
                    if (distanceToWall < closestDistance)
                    {
                        closestWall = wall.gameObject;
                        closestDistance = distanceToWall;
                    }
                }
                Vector3 lookPos = new Vector3(closestWall.transform.position.x, 0f, closestWall.transform.position.z);
                fridge.transform.rotation = Quaternion.LookRotation(-lookPos);
            }
        }
        if (oven != null)
        {
            oven.transform.position = posOven;
            if (walls != null)
            {
                GameObject closestWall = null;
                float closestDistance = Mathf.Infinity;

                foreach (Transform wall in walls)
                {
                    float distanceToWall = Vector3.Distance(oven.transform.position, wall.transform.position);
                    if (distanceToWall < closestDistance)
                    {
                        closestWall = wall.gameObject;
                        closestDistance = distanceToWall;
                    }
                }
                Vector3 lookPos = new Vector3(closestWall.transform.position.x, 0f, closestWall.transform.position.z);
                oven.transform.rotation = Quaternion.LookRotation(-lookPos);
            }
        }
        if (lucka != null)
        {
            lucka.transform.position = posLucka;
            if (walls != null)
            {
                GameObject closestWall = null;
                float closestDistance = Mathf.Infinity;

                foreach (Transform wall in walls)
                {
                    float distanceToWall = Vector3.Distance(lucka.transform.position, wall.transform.position);
                    if (distanceToWall < closestDistance)
                    {
                        closestWall = wall.gameObject;
                        closestDistance = distanceToWall;
                    }
                }
                lucka.transform.rotation = closestWall.transform.rotation;
            }
        }
        if (door != null)
        {
            door.transform.position = posDoor;
            if (walls != null)
            {
                GameObject closestWall = null;
                float closestDistance = Mathf.Infinity;

                foreach (Transform wall in walls)
                {
                    float distanceToWall = Vector3.Distance(door.transform.position, wall.transform.position);
                    if (distanceToWall < closestDistance)
                    {
                        closestWall = wall.gameObject;
                        closestDistance = distanceToWall;
                    }
                }
                Vector3 lookPos = new Vector3(closestWall.transform.position.x, 0f, closestWall.transform.position.z);
                door.transform.rotation = closestWall.transform.rotation;
            }
        }
        if (fridge != null)
        {
            fridge.GetComponentInChildren<Fridge>().MovedDone();
        }
        if (table != null)
        {
            table.GetComponent<Counters>().SetNonKinematic();
        }
    }
}
