using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    private ParticleSystem cheeseParticle = null;


    private void Awake()
    {
        cheeseParticle = GetComponentInChildren<ParticleSystem>();
    }


}
