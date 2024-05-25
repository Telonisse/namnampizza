using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] GameObject soundObject;

    public void Grabbed()
    {
        soundObject.SetActive(true);
    }
    public void Dropped()
    {
        soundObject.SetActive(false);
    }
}
