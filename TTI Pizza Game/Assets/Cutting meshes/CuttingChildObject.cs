using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingChildObject : MonoBehaviour
{
    public Transform myChildObject;
    public GameObject parent;


    public bool detachChild;
    public bool isOnMyPizza = false;


    void Update()
    {
        if (detachChild == true)
        {
            myChildObject.parent = null;
            detachChild = false;
            Debug.Log("detach false");



        }

        if (parent.transform.childCount == 1 && parent.name == transform.parent.name)
        {
            Debug.Log("detach true");
            detachChild = true;
            gameObject.GetComponent<Grabbable>().enabled = true;
            gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
            gameObject.GetComponent<TouchHandGrabInteractable>().enabled = true;
            gameObject.GetComponent<MeshCollider>().enabled = true;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;


        }
    }

    public void deactivateObject(GameObject obj)
    {
        //if (obj == null)
        //{
        //return;

        //}
        if (obj.transform.childCount == 0)
        {
            obj.SetActive(false);
            //Destroy(parent);
            //parent = null;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            if (isOnMyPizza == true)
            {
                return;
            }
            else
            {
                detachChild = true;
                gameObject.GetComponent<Grabbable>().enabled = true;
                gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                gameObject.GetComponent<TouchHandGrabInteractable>().enabled = true;
                gameObject.GetComponent<MeshCollider>().enabled = true;

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;

                deactivateObject(parent);
            }
            
        }

        if (other.gameObject.CompareTag("Pizza"))
        {
            isOnMyPizza = true;
            //gameObject.GetComponent<Grabbable>().enabled = false;
            //gameObject.GetComponent<PhysicsGrabbable>().enabled = false;
            //gameObject.GetComponent<TouchHandGrabInteractable>().enabled = false;
            //gameObject.GetComponent<MeshCollider>().enabled = false;

            //Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            //rb.useGravity = false;
            //gameObject.GetComponent<BoxCollider>().enabled = false;
           
            //rb.isKinematic = true;

            // Turn off box collider?

            deactivateObject(parent);

        }
    }
}
