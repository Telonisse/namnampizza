using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counters : MonoBehaviour
{
    [SerializeField] GameObject knife;
    private Vector3 startPosKnife;
    [SerializeField] GameObject tomatoSauce;
    private Vector3 startPosTomato;
    [SerializeField] GameObject rivjarn;
    private Vector3 startPosRiv;

    private void Start()
    {
        startPosKnife = knife.transform.position;
        startPosTomato = tomatoSauce.transform.position;
        startPosRiv = rivjarn.transform.position;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, knife.transform.position) >= 5)
        {
            knife.transform.position = startPosKnife;
        }
        if (Vector3.Distance(transform.position, tomatoSauce.transform.position) >= 5)
        {
            tomatoSauce.transform.position = startPosTomato;
        }
        if (Vector3.Distance(transform.position, rivjarn.transform.position) >= 5)
        {
            rivjarn.transform.position = startPosRiv;
        }
    }
}
