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

        knife.GetComponent<Rigidbody>().isKinematic = true;
        tomatoSauce.GetComponent<Rigidbody>().isKinematic = true;
        rivjarn.GetComponent<Rigidbody>().isKinematic = true;
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

    public void SetNonKinematic()
    {
        knife.GetComponent<Rigidbody>().isKinematic = false;
        tomatoSauce.GetComponent<Rigidbody>().isKinematic = false;
        rivjarn.GetComponent<Rigidbody>().isKinematic = false;
    }
}
