using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Meta.XR.MRUtilityKit.MRUK;
using static Unity.VisualScripting.Metadata;

public class FindSpawnPos : MonoBehaviour
{
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] GameObject fridge;
    [SerializeField] Transform[] roomObjects;

    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private MRUKRoom room;
    int numOfWalls = 0;
    private bool fridgeSpawned = false;

    public void SpawnObject()
    {
        FindSpawnPosOnSurface();
    }

    private void FindSpawnPosOnSurface()
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
            }
        }
        //check what type of object and spawn object on it
        foreach (Transform child in roomObjects)
        {
            if (child.GetComponent<MRUKAnchor>().HasLabel("TABLE") || child.GetComponent<MRUKAnchor>().HasLabel("OTHER"))
            {
                spawnPos = child.transform.position;
                spawnPos.y = child.transform.position.y - (spawnPrefab.transform.localScale.y / 2);
                spawnRot = child.transform.rotation;
                Instantiate(spawnPrefab, spawnPos, spawnRot, transform);
            }
        }

        //spawn fridge on random wall
        int randomWall = Random.Range(0, numOfWalls);
        spawnPos = roomObjects[randomWall].transform.position;
        spawnPos.y = roomObjects[randomWall].transform.position.y - (spawnPrefab.transform.localScale.y / 2);
        spawnPos.z = roomObjects[randomWall].transform.position.z - (spawnPrefab.transform.localScale.z / 2);
        spawnRot = roomObjects[randomWall].transform.rotation * Quaternion.Euler(0, 90, 0);
        Instantiate(fridge, spawnPos, spawnRot, transform);
    }
}
