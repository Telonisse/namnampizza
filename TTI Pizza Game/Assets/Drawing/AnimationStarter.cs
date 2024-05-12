using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStarter : MonoBehaviour
{
    //[SerializeField] private Animator animationControllerForCheese;
    [SerializeField] Animator animationControllerForTomatoSauce;
   // public bool tomatoSaucePouring = false;

    public void PauseTomatoSauceAnimation()
    {
       animationControllerForTomatoSauce.SetFloat("PourValue", 0.0f);

    }
    private void Update()
    {
        
    }

    public void PlayTomatoSauceAnimation()
    {
       animationControllerForTomatoSauce.SetBool("IsPouring", true);
       animationControllerForTomatoSauce.SetFloat("PourValue", 1.0f);
    }





}
