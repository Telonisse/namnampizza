using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingChildObject : MonoBehaviour
{
    public Transform myChildObject;
    public GameObject parent;


    bool detachChild;
    // public bool isOnMyPizza = false;
    public bool beenCut = false;
    [SerializeField] GameObject soundObject;


    void Update()
    {
        if (detachChild == true)
        {
            myChildObject.parent = null;
            detachChild = false;
            Debug.Log("detach false");

        }

        if (parent != null && transform.parent != null)
            if (parent.transform.childCount == 3 && parent.name == transform.parent.name) 
        {

            Debug.Log("detach true");
            detachChild = true;
            gameObject.GetComponent<Grabbable>().enabled = true;
            gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
            gameObject.GetComponent<TouchHandGrabInteractable>().enabled = true;
            gameObject.GetComponent<MeshCollider>().enabled = true;
            parent.GetComponent<BoxCollider >().enabled = false;

            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            beenCut = true;


        }
    }

    public void deactivateObject(GameObject obj)
    {
        //if (obj == null)
        //{
        //return;

        //}
        if (obj.transform.childCount == 2)
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
            PizzaScript pizza = other.gameObject.GetComponent<PizzaScript>();
            if (pizza != null && pizza.meAmOnPizza)
            {
                return;
            }
            else
            {
                soundObject.SetActive(true);
                detachChild = true;
                gameObject.GetComponent<Grabbable>().enabled = true;
                gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                gameObject.GetComponent<TouchHandGrabInteractable>().enabled = true;
                gameObject.GetComponent<MeshCollider>().enabled = true;

                Rigidbody rb = gameObject.GetComponent<Rigidbody>();
                rb.useGravity = true;
                rb.isKinematic = false;

                deactivateObject(parent);
                beenCut = true;
                soundObject.SetActive(false);
            }
            
        }
        if (other.gameObject.CompareTag("Pizza") )
        {
            
            //isOnMyPizza = true;
            //parent.SetActive(false);
           // deactivateObject(parent);

        }
    }
}
