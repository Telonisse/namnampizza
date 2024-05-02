using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToLocation : MonoBehaviour
{
    private bool grabbed;
    private bool insideSnapZone;
    public bool Snapped;

    public GameObject Block;
    public GameObject SnapRotaionRef;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == Block.name)
        {
            insideSnapZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == Block.name)
        {
            insideSnapZone = false;
        }
    }

    void SnapObject()
    {
        if(grabbed == false && insideSnapZone == true)
        {
            Block.gameObject.transform.position = transform.position;
            Block.gameObject.transform.rotation = SnapRotaionRef.transform.rotation;
            Snapped = true;
        }
    }
    
}
