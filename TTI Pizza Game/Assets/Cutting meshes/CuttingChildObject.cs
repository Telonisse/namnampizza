using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingChildObject : MonoBehaviour
{
    public Transform myChildObject;

    public bool detachChild;

    public GameObject parent;

    bool isDestroyed = false;


    void Update()
    {
        if (isDestroyed == true)
        {
            detachChild = false;
            parent = null;
            return;
        }
        else
        {
            if (detachChild == true)
            {
                myChildObject.parent = null;
                detachChild = false;
                deactivateObject(parent);

            }
            if (parent != null && parent.transform.childCount == 1)
            {
                detachChild = true;
                activateObject();
            }
            //if (parent.transform.childCount == 1)
            //{
            //    detachChild = true;
            //    activateObject();
            //    //parent = null;

            //}
        }
        
    }

    public void deactivateObject(GameObject obj)
    {
        //if (obj == null)
        //{
        //return;

        //}
        if (isDestroyed == true)
        {
            return;
        }
        else
        {
            if (obj.transform.childCount == 0)
            {
                //obj.SetActive(false);
                Destroy(parent);
                isDestroyed = true;
                parent = null;
            }
        }

  

        //if (obj != null && obj.transform.childCount == 0)
        //{
        //    Destroy(obj);
        //    parent = null; 
        //}
    }

    void activateObject()
    {
        gameObject.GetComponent<Grabbable>().enabled = true;
        gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
        gameObject.GetComponent<TouchHandGrabInteractable>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            detachChild = true;

            activateObject();

            deactivateObject(parent);

        }

        if (other.gameObject.CompareTag("Pizza"))
        {
            gameObject.GetComponent<Grabbable>().enabled = false;
            gameObject.GetComponent<PhysicsGrabbable>().enabled = false;
            gameObject.GetComponent<TouchHandGrabInteractable>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;

            deactivateObject(parent);
        }
    }
}
