using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class CheeseGrater : MonoBehaviour
{
    public Transform cheeseOrigin = null;

    //[SerializeField] Animator myAnimationControllerCheese;
    public string collisionTag = "Pizza";

    [SerializeField] private ParticleSystem myParticleSystem;
    private bool cheeseTouch = false;
    private AnimationStarter prevAnim = null;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cheeseOrigin.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(collisionTag))
            {
                if (cheeseTouch == true)
                {
                    //myParticleSystem.Play();
                    prevAnim = hit.transform.GetComponentInChildren<AnimationStarter>();
                    prevAnim.PlayGratingCheeseAnimation();
                }
                else
                {
                    myParticleSystem.Stop();
                    hit.transform.GetComponentInChildren<AnimationStarter>().PauseCheeseGratingAnimation();
                }
            }
            else
            {
                if (prevAnim != null)
                {
                    if (myParticleSystem.isPlaying)
                        myParticleSystem.Stop();
                    
                    prevAnim.PauseCheeseGratingAnimation();
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Cheese"))
        {
            myParticleSystem.Play();
            cheeseTouch = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        cheeseTouch = false;

    }
}
