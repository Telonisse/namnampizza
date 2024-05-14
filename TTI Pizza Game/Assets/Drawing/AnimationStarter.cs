using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStarter : MonoBehaviour
{
    [SerializeField] Animator animationControllerForCheese;
    [SerializeField] Animator animationControllerForTomatoSauce;

    private static AnimationStarter instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void PauseTomatoSauceAnimation()
    {
       animationControllerForTomatoSauce.SetFloat("PourValue", 0.0f);

    }
    public void PauseCheeseGratingAnimation()
    {
        animationControllerForCheese.SetFloat("GrateValue", 0.0f);


    }

    public void PlayTomatoSauceAnimation()
    {
       animationControllerForTomatoSauce.SetBool("IsPouring", true);
       animationControllerForTomatoSauce.SetFloat("PourValue", 1.0f);
    }
    public void PlayGratingCheeseAnimation()
    {
        animationControllerForCheese.SetBool("IsGrating", true);
        animationControllerForCheese.SetFloat("GrateValue", 1.0f);
    }





}
