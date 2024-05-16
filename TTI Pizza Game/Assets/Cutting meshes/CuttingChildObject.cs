using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingChildObject : MonoBehaviour
{
    public Transform myChildObject;

     bool detachChild;

    public GameObject parent;

     bool isDestroyed = false;
     bool isOnMyPizza = false;


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
                deleteObject(parent);

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

    public void deleteObject(GameObject obj)
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
            if (isOnMyPizza == true)
            {
                Destroy(parent);
                isDestroyed = true;
                parent = null;
                isOnMyPizza=false;
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
        if (isOnMyPizza == true)
        {
            return; 
        }
        else
        {
            if (other.gameObject.CompareTag("Knife"))
            {
                detachChild = true;

                activateObject();

                deleteObject(parent);

            }
        }
        

        if (other.gameObject.CompareTag("Pizza"))
        {
            isOnMyPizza = true;
            gameObject.GetComponent<Grabbable>().enabled = false;
            gameObject.GetComponent<PhysicsGrabbable>().enabled = false;
            gameObject.GetComponent<TouchHandGrabInteractable>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;

            if (myChildObject != null)
                myChildObject.parent = null;
        }
    }
}
