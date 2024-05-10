using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CheeseGrater : MonoBehaviour
{
    public Transform cheeseOrigin = null;

    [SerializeField] private Animator myAnimationController;
    public string collisionTag = "Tomatosauce";

    [SerializeField] private ParticleSystem myParticleSystem;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cheese"))
        {
            Debug.Log("touched");
            myParticleSystem.Play();

            RaycastHit hit;
            if (Physics.Raycast(cheeseOrigin.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(collisionTag))
                {
                    Debug.Log("Collision detected with tag: " + collisionTag);
                    myAnimationController.SetBool("IsGrating", true);
                    myAnimationController.SetFloat("GrateValue", 1.0f);


                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        myAnimationController.SetFloat("GrateValue", 0.0f);
        myParticleSystem.Stop();

    }
}
