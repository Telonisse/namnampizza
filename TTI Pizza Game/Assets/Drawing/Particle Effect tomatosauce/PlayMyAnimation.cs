using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMyAnimation : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;
    public ParticleSystem myParticleSystem;

    private void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Touched");

        if (other.CompareTag("TomatoSauceCan"))
        {
            Debug.Log("PLEASETOUCH");
            myAnimationController.SetBool("IsPouring", true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TomatoSauceCan"))
        {
            myAnimationController.SetBool("IsPouring", false);
        }

    }
}
