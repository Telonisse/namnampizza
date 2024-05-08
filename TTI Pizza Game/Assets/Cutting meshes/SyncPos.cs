using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPos : MonoBehaviour
{
    public GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        // Check if the target object exists
        if (targetObject != null)
        {
            // Set the position of this object to match the position of the target object
            transform.position = targetObject.transform.position;
        }
    }
}
