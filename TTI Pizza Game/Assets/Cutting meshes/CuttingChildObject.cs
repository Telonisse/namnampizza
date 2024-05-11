using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingChildObject : MonoBehaviour
{
    public Transform myChildObject;

    public bool detachChild;

    public GameObject Parent;

    void Update()
    {
        if (detachChild == true)
        {
            myChildObject.parent = null;
        }
    }

    public void deactivateObject(GameObject obj)
    {
        //if (obj == null)
        //{
         //   return;
        //}
        if (obj.transform.childCount == 0)
        {
            obj.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            detachChild = true;
            gameObject.GetComponent<Grabbable>().enabled = true;
            gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
            gameObject.GetComponent<TouchHandGrabInteractable>().enabled = true;
            gameObject.GetComponent<MeshCollider>().enabled = true;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;

            deactivateObject(Parent);
        }

    }
}
