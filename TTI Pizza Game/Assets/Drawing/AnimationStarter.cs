using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStarter : MonoBehaviour
{
    [SerializeField] Animator animationControllerForCheese;
    [SerializeField] Animator animationControllerForTomatoSauce;

    private static AnimationStarter instance;

    // public bool tomatoSaucePouring = false;

    public static AnimationStarter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AnimationStarter>();
                if (instance == null)
                {
                    Debug.LogError("AnimationStarter instance not found in the scene.");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
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
