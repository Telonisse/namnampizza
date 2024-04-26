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
    public int currentWall = 0;

    //spawn fridge
    private GameObject spawnedFridge = null;

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
        spawnRot = roomObjects[0].transform.rotation * Quaternion.Euler(0, 0, 0);
        spawnedFridge = Instantiate(fridge, spawnPos, spawnRot, transform);
    }
    private void Update()
    {
        boxCenter = spawnedFridge.transform.position;
        boxSize = spawnedFridge.transform.localScale;
        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2f, spawnedFridge.transform.rotation);

        if (colliders.Length > 0)
        {
            Debug.Log(colliders.Length);
            for (int i = 0; i < colliders.Length; i++)
            {
                MRUKAnchor anchor = colliders[i].GetComponentInParent<MRUKAnchor>();
                if (anchor != null &&  !anchor.HasLabel("WALL_FACE"))
                {
                    float z = spawnedFridge.transform.localPosition.z - 0.01f;
                    //spawnedFridge.transform.localPosition = new Vector3(spawnedFridge.transform.localPosition.x, spawnedFridge.transform.localPosition.y, z);
                    //spawnedFridge.transform.Translate(0, 0, 0.01f);
                    Vector3 pos = spawnedFridge.transform.position;
                    pos += anchor.transform.right * Time.deltaTime *0.1f;
                    spawnedFridge.transform.position = pos;

                    spawnedFridge.transform.rotation = roomObjects[currentWall].transform.rotation;


                }
                //if (anchor != null && roomObjects[currentWall].GetComponent<MRUKAnchor>().PlaneBoundary2D[1].x < spawnedFridge.transform.localPosition.z)
                //{
                //    Vector2 wa = colliders[i].GetComponentInParent<MRUKAnchor>().PlaneBoundary2D[0];
                //    float x = wa.x * -1;
                //    Debug.Log(x);
                //    if (spawnedFridge.transform.localPosition.z > x)
                //    {
                //        currentWall++;
                //        spawnedFridge.transform.position = new Vector3(roomObjects[currentWall].transform.position.x, roomObjects[currentWall].transform.position.y - (spawnPrefab.transform.localScale.y / 2), roomObjects[currentWall].transform.position.z - (spawnPrefab.transform.localScale.z / 2));
                //        spawnedFridge.transform.rotation = roomObjects[currentWall].transform.rotation * Quaternion.Euler(0, 90, 0);
                //    }
                //}
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
