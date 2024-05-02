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
    [SerializeField] GameObject oven;
    [SerializeField] Transform[] roomObjects;
    [SerializeField] Transform floor;

    private Vector3 spawnPos;
    private Quaternion spawnRot;
    private MRUKRoom room;
    int numOfWalls = 0;
    public int currentWall = 0;

    //spawn fridge
    private GameObject spawnedFridge = null;

    private Vector3 boxCenterFridge;
    private Vector3 boxSizeFridge;
    private float movedFridge = 0;
    private float maxMoveFridge = 0;
    private bool moveOnceFridge = false;

    //spawn oven
    private GameObject spawnedOven = null;

    private Vector3 boxCenterOven;
    private Vector3 boxSizeOven;
    private float movedOven = 0;
    private float maxMoveOven = 0;
    private bool moveOnceOven = false;

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
        maxMoveFridge = roomObjects[0].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
        spawnPos.x = roomObjects[0].transform.position.x;
        spawnPos.y = roomObjects[0].transform.position.y;
        spawnPos.z = roomObjects[0].transform.position.z;
        spawnRot = roomObjects[0].transform.rotation;
        spawnedFridge = Instantiate(fridge, spawnPos, spawnRot, transform);
        if (!moveOnceFridge)
        {
            spawnedFridge.transform.Translate(-maxMoveFridge, 0, 0.5f, Space.Self);
            moveOnceFridge = true;
        }
        //spawn oven
        maxMoveOven = roomObjects[0].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
        spawnPos.x = roomObjects[0].transform.position.x;
        spawnPos.y = roomObjects[0].transform.position.y;
        spawnPos.z = roomObjects[0].transform.position.z;
        spawnRot = roomObjects[0].transform.rotation;
        spawnedOven = Instantiate(oven, spawnPos, spawnRot, transform);
        if (!moveOnceOven)
        {
            spawnedOven.transform.Translate(-maxMoveOven, 0, 0.5f, Space.Self);
            moveOnceOven = true;
        }
    }
    private void Update()
    {
        //FIND POS FRIDGE
        boxCenterFridge = new Vector3(spawnedFridge.transform.position.x, spawnedFridge.transform.position.y + 0.6f, spawnedFridge.transform.position.z);
        boxSizeFridge = new Vector3(0.7f, 1.2f, 0.7f);
        Collider[] collidersFridge = Physics.OverlapBox(boxCenterFridge, boxSizeFridge / 2f, spawnedFridge.transform.rotation);

        spawnedFridge.transform.position = new Vector3(spawnedFridge.transform.position.x, 0, spawnedFridge.transform.position.z);

        if (collidersFridge.Length > 0)
        {
            Debug.Log(collidersFridge.Length);
            for (int i = 0; i < collidersFridge.Length; i++)
            {
                MRUKAnchor anchor = collidersFridge[i].GetComponentInParent<MRUKAnchor>();
                if (anchor != null)
                {
                    maxMoveFridge = roomObjects[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
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
                    movedFridge = 0;
                    moveOnceFridge = false;
                    maxMoveFridge = roomObjects[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                    spawnedFridge.transform.position = new Vector3(roomObjects[currentWall].transform.position.x, roomObjects[currentWall].transform.position.y, roomObjects[currentWall].transform.position.z);
                    spawnedFridge.transform.rotation = roomObjects[currentWall].transform.rotation * Quaternion.Euler(0, 0, 0);
                    if (!moveOnceFridge)
                    {
                        spawnedFridge.transform.Translate(-maxMoveFridge, 0, 0.5f, Space.Self);
                        moveOnceFridge = true;
                    }
                }
            }
        }

        //FIND POS OVEN
        boxCenterOven = new Vector3(spawnedOven.transform.position.x, spawnedOven.transform.position.y + 0.6f, spawnedOven.transform.position.z);
        boxSizeOven = new Vector3(0.8f, 1.2f, 0.8f);
        Collider[] collidersOven = Physics.OverlapBox(boxCenterOven, boxSizeOven / 2f, spawnedOven.transform.rotation);

        spawnedOven.transform.position = new Vector3(spawnedOven.transform.position.x, 0 + 0.0001f, spawnedOven.transform.position.z);

        if (collidersOven.Length > 0)
        {
            Debug.Log(collidersOven.Length);
            for (int i = 0; i < collidersOven.Length; i++)
            {
                MRUKAnchor anchor = collidersOven[i].GetComponentInParent<MRUKAnchor>();
                if (anchor != null)
                {
                    maxMoveOven = roomObjects[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                }
                if (anchor != null && !anchor.HasLabel("WALL_FACE"))
                {
                    spawnedOven.transform.Translate(0.01f, 0, 0, Space.Self);
                    movedOven += 0.01f;
                }
                if (anchor != null && maxMoveOven < movedOven)
                {
                    movedOven = 0;
                    currentWall++;
                    movedOven = 0;
                    moveOnceOven = false;
                    maxMoveOven = roomObjects[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x;
                    spawnedOven.transform.position = new Vector3(roomObjects[currentWall].transform.position.x, roomObjects[currentWall].transform.position.y, roomObjects[currentWall].transform.position.z);
                    spawnedOven.transform.rotation = roomObjects[currentWall].transform.rotation * Quaternion.Euler(0, 0, 0);
                    if (!moveOnceOven)
                    {
                        spawnedOven.transform.Translate(-maxMoveOven, 0, 0.5f, Space.Self);
                        moveOnceOven = true;
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the overlap box in the Scene view for visualization
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(boxCenterOven, spawnedOven.transform.rotation, boxSizeOven);
        Gizmos.matrix = rotationMatrix;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
