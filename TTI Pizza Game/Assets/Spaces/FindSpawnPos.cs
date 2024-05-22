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
    [SerializeField] GameObject previewFridge;
    [SerializeField] GameObject oven;
    [SerializeField] GameObject previewOven;
    [SerializeField] GameObject lucka;
    [SerializeField] GameObject previewLucka;    
    [SerializeField] GameObject door;
    [SerializeField] GameObject previewDoor;
    [SerializeField] Transform[] roomObjects;
    [SerializeField] Transform floor;

    private GameObject currentPreview;

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

    //spawn door
    private GameObject spawnedDoor = null;

    private bool doorDone = false;

    //spawn fridge
    private GameObject spawnedFridge = null;

    private bool fridgeDone = false;

    //spawn oven
    private GameObject spawnedOven = null;

    private bool ovenDone = false;

    //Spawn lucka
    private GameObject spawnedLucka = null;

    private bool luckaDone = false;

    private bool roomLoaded = false;
    private bool wasButtonPressed = false;
    private GameController gameController;
    private FadeScreen fade;

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
        roomLoaded = true;
    }
    private void Start()
    {
        currentPreview = Instantiate(previewFridge);
        gameController = FindObjectOfType<GameController>();
        fade = FindObjectOfType<FadeScreen>();
    }
    private void Update()
    {
        if (roomLoaded)
        {
            bool isButtonPressed = OVRInput.GetDown(OVRInput.Button.One);
            Ray ray = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch), OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.transform.name);
                currentPreview.transform.position = hit.point;
                currentPreview.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                if (OVRInput.GetDown(OVRInput.Button.One) && !fridgeDone && !wasButtonPressed && isButtonPressed && hit.transform.GetComponentInParent<MRUKAnchor>().HasLabel("FLOOR")) //&& hit.transform.GetComponentInParent<MRUKAnchor>().HasLabel("FLOOR")
                {
                    if (hit.transform.GetComponentInParent<MRUKAnchor>() != null)
                    {
                        if (hit.transform.GetComponentInParent<MRUKAnchor>().HasLabel("FLOOR"))
                        {
                            spawnedFridge = Instantiate(fridge, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            //save pos
                            fridgeDone = true;
                            Destroy(currentPreview);
                            currentPreview = Instantiate(previewOven);
                            wasButtonPressed = isButtonPressed;
                        }
                    }
                }
                if (OVRInput.GetDown(OVRInput.Button.One) && !ovenDone && fridgeDone && !wasButtonPressed && isButtonPressed) //
                {
                    if (hit.transform.GetComponentInParent<MRUKAnchor>() != null)
                    {
                        if (hit.transform.GetComponentInParent<MRUKAnchor>().HasLabel("FLOOR"))
                        {
                            Debug.Log("STOP SPAWN OVEN PLSPSLPSL");
                            spawnedOven = Instantiate(oven, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            //save pos
                            ovenDone = true;
                            Destroy(currentPreview);
                            currentPreview = Instantiate(previewDoor);
                            wasButtonPressed = isButtonPressed;
                        }
                    }
                }
                if (OVRInput.GetDown(OVRInput.Button.One) && fridgeDone && ovenDone && !doorDone && !wasButtonPressed && isButtonPressed) //&& hit.transform.GetComponentInParent<MRUKAnchor>().HasLabel("FLOOR")
                {
                    if (hit.transform.GetComponentInParent<MRUKAnchor>() != null)
                    {
                        if (hit.transform.GetComponentInParent<MRUKAnchor>().HasLabel("FLOOR"))
                        {
                            spawnedDoor = Instantiate(door, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            //save pos
                            doorDone = true;
                            Destroy(currentPreview);
                            currentPreview = Instantiate(previewLucka);
                            wasButtonPressed = isButtonPressed;
                        }
                    }
                }
                if (OVRInput.GetDown(OVRInput.Button.One) && fridgeDone && ovenDone && doorDone && !luckaDone && !wasButtonPressed && isButtonPressed)
                {
                    if (hit.transform.GetComponentInParent<MRUKAnchor>() != null)
                    {
                        if (hit.transform.GetComponentInParent<MRUKAnchor>().HasLabel("WALL_FACE"))
                        {
                            spawnedLucka = Instantiate(lucka, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            //save pos
                            luckaDone = true;
                        }
                    }
                }
                wasButtonPressed = isButtonPressed;
            }

            if (luckaDone && ovenDone && fridgeDone && doorDone)
            {
                gameController.SavePos(spawnedFridge.transform.position, spawnedOven.transform.position, spawnedLucka.transform.position, spawnedDoor.transform.position);
                StartCoroutine(FurnitureDone());
            }
        }
    }
    private IEnumerator FurnitureDone()
    {
        fade.FadeOut(false);
        yield return new WaitForSecondsRealtime(2);
    }
}
