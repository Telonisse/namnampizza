using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CheeseGrater : MonoBehaviour
{
    public Transform cheeseOrigin = null;

    [SerializeField] Animator myAnimationControllerCheese;
    public string collisionTag = "Pizza";

    [SerializeField] private ParticleSystem myParticleSystem;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cheese"))
        {
            //Debug.Log("touched");
            myParticleSystem.Play();

            RaycastHit hit;
            if (Physics.Raycast(cheeseOrigin.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(collisionTag))
                {
                    //Debug.Log("Collision detected with tag: " + collisionTag);
                    //myAnimationControllerCheese.SetBool("IsGrating", true);
                    //myAnimationControllerCheese.SetFloat("GrateValue", 1.0f);
                    AnimationStarter.Instance.PlayGratingCheeseAnimation();


                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        AnimationStarter.Instance.PauseCheeseGratingAnimation();

        //myAnimationControllerCheese.SetFloat("GrateValue", 0.0f);
        myParticleSystem.Stop();

    }
}
