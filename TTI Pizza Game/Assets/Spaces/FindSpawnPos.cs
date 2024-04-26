using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Meta.XR.MRUtilityKit.MRUK;

public class FindSpawnPos : MonoBehaviour
{
    [SerializeField] GameObject spawnPrefab;
    [SerializeField] GameObject fridge;
    [SerializeField] Transform[] roomObjects;

    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private MRUKRoom room;
    int numOfWalls = 0;

    //spawn fridge
    private GameObject spawnedFridge = null;

    public bool fridgeUnObstructed = false;

    [SerializeField] Vector3 boxCenter;
    [SerializeField] Vector3 boxSize;

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

        //spawn fridge and check walls until it doesnt collide with any other object
        spawnPos = roomObjects[0].transform.position;
        spawnPos.y = roomObjects[0].transform.position.y - (spawnPrefab.transform.localScale.y / 2);
        spawnPos.z = roomObjects[0].transform.position.z - (spawnPrefab.transform.localScale.z / 2);
        spawnRot = roomObjects[0].transform.rotation * Quaternion.Euler(0, 90, 0);
        spawnedFridge = Instantiate(fridge, spawnPos, spawnRot, transform);

        boxCenter = spawnedFridge.transform.position;
        boxSize = spawnedFridge.transform.localScale;
    }
    private void Update()
    {
        Collider[] colliders = null;
        if (fridgeUnObstructed == false)
        {
            colliders = Physics.OverlapBox(boxCenter, boxSize / 2f, spawnedFridge.transform.rotation);
            fridgeUnObstructed = true;
        }

        if (colliders.Length > 0)
        {
            Debug.Log(colliders.Length);
            for (int i = 0; i < colliders.Length; i++)
            {
                Debug.Log(colliders[i].ToString());
                if (colliders[i].GetComponentInParent<MRUKAnchor>() != null)
                {
                    
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the overlap box in the Scene view for visualization
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(boxCenter, spawnedFridge.transform.rotation, boxSize);
        Gizmos.matrix = rotationMatrix;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
