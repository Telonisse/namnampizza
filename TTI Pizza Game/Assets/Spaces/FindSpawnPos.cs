using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Meta.XR.MRUtilityKit.MRUK;

public class FindSpawnPos : MonoBehaviour
{
    [SerializeField] GameObject table;
    [SerializeField] GameObject fridge;
    [SerializeField] Transform[] roomObjects;

    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private MRUKRoom room;
    int numOfWalls = 0;
    public int currentWall = 0;

    //spawn fridge
    private GameObject spawnedFridge = null;

    [SerializeField] Vector3 boxCenter;
    [SerializeField] Vector3 boxSize;
    private float moved = 0;
    private float maxMove = 0;
    private bool moveOnce = false;

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
                spawnPos.y = child.transform.position.y - (table.transform.localScale.y / 2);
                spawnRot = child.transform.rotation;
                Instantiate(table, spawnPos, spawnRot, transform);
            }
        }

        //spawn fridge and check walls until it doesnt collide with any other object
        maxMove = roomObjects[0].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
        spawnPos.x = roomObjects[0].transform.position.x;
        spawnPos.y = roomObjects[0].transform.position.y;
        spawnPos.z = roomObjects[0].transform.position.z;
        spawnRot = roomObjects[0].transform.rotation;
        spawnedFridge = Instantiate(fridge, spawnPos, spawnRot, transform);
        if (!moveOnce)
        {
            spawnedFridge.transform.Translate(-maxMove, 0, 0.5f, Space.Self);
            moveOnce = true;
        }
    }
    private void Update()
    {
        boxCenter = spawnedFridge.transform.position;
        boxSize = spawnedFridge.transform.GetChild(0).localScale;
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2f, spawnedFridge.transform.rotation);

        RaycastHit hit;
        Vector3 rayVec = new Vector3(spawnedFridge.transform.position.x, spawnedFridge.transform.position.y - 1, spawnedFridge.transform.position.y);
        if (Physics.Raycast(rayVec, -Vector3.up, out hit, 10f))
        {
            // If the ray hits something, log the name of the object it hits
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            spawnedFridge.transform.position = new Vector3(spawnedFridge.transform.position.x, hit.collider.transform.position.y + 1.00001f, spawnedFridge.transform.position.z);
        }

        if (colliders.Length > 0)
        {
            Debug.Log(colliders.Length);
            for (int i = 0; i < colliders.Length; i++)
            {
                MRUKAnchor anchor = colliders[i].GetComponentInParent<MRUKAnchor>();
                if (anchor != null)
                {
                    maxMove = roomObjects[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                }
                if (anchor != null && !anchor.HasLabel("WALL_FACE"))
                {
                    spawnedFridge.transform.Translate(0.01f, 0, 0, Space.Self);
                    moved += 0.01f;
                }
                if (anchor != null && maxMove < moved)
                {
                    moved = 0;
                    currentWall++;
                    moved = 0;
                    moveOnce = false;
                    maxMove = roomObjects[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                    spawnedFridge.transform.position = new Vector3(roomObjects[currentWall].transform.position.x, roomObjects[currentWall].transform.position.y, roomObjects[currentWall].transform.position.z);
                    spawnedFridge.transform.rotation = roomObjects[currentWall].transform.rotation * Quaternion.Euler(0, 0, 0);
                    if (!moveOnce)
                    {
                        spawnedFridge.transform.Translate(-maxMove, 0, 0.5f, Space.Self);
                        moveOnce = true;
                    }
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
