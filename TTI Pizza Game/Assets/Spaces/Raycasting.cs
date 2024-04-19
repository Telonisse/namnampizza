using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEditor;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public int rayLength = 10;
    public float delay = 0.1f;
    bool aboutToTeleport = false;
    Vector3 teleportPos = new Vector3();

    public Material tMat;

    // I dont like spaces it makes me sad :(

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayLength * 10))
            {
                aboutToTeleport = true;
                teleportPos = hit.point;

                GameObject myLine = new GameObject();
                myLine.transform.position = teleportPos;
                myLine.AddComponent<LineRenderer>();

                LineRenderer lineRenderer = myLine.AddComponent<LineRenderer>();
                lineRenderer.material = tMat;

                lineRenderer.startWidth = 0.01f;
                lineRenderer.endWidth = 0.01f;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
                GameObject.Destroy(myLine, delay);
            }
        }
    }
}
