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

    public bool selected = false;

    public float timer = 0f;
    [SerializeField] float maxTimer = 3f;

    private void Awake()
    {
        knife.GetComponent<Rigidbody>().isKinematic = true;
        tomatoSauce.GetComponent<Rigidbody>().isKinematic = true;
        rivjarn.GetComponent<Rigidbody>().isKinematic = true;
    }
    private void Start()
    {
    }
    void Update()
    {
        if (selected == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= maxTimer && selected == true)
        {
            Debug.Log("Timer Stopped");
            timer = 0f;
            knife.transform.position = startPosKnife;
            tomatoSauce.transform.position = startPosTomato;
            rivjarn.transform.position = startPosRiv;
        }
    }

    public void SelectThumbsUp()
    {
        selected = true;
    }
    public void DeselectThumbsUp()
    {
        selected = false;
        timer = 0f;
    }

    public void SetNonKinematic()
    {
        knife.GetComponent<Rigidbody>().isKinematic = false;
        tomatoSauce.GetComponent<Rigidbody>().isKinematic = false;
        rivjarn.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("kinematic set");
        startPosKnife = knife.transform.position;
        startPosTomato = tomatoSauce.transform.position;
        startPosRiv = rivjarn.transform.position;
    }
}
