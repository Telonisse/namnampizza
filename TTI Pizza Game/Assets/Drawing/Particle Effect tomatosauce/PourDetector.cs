using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    [SerializeField] Animator myAnimationControllerTomatoSauce; 
    public string collisionTag = "Pizza";

    private bool IsPouring = false;
    private Stream currentStream = null;

    //public  AnimationStarter animationStarter;

    private void Start()
    {
        //animationStarter = GetComponentInChildren<AnimationStarter>();

    }

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if(IsPouring != pourCheck)
        {
            IsPouring = pourCheck;

            if (IsPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
                myAnimationControllerTomatoSauce.SetFloat("PourValue", 0.0f);
            }
        }

        if (IsPouring)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(collisionTag))
                {
                    Debug.Log("Collision detected with tag: " + collisionTag);
                    myAnimationControllerTomatoSauce.SetBool("IsPouring", true);
                    myAnimationControllerTomatoSauce.SetFloat("PourValue", 1.0f);

                }
            }
        }
    }

    private void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
        
    }

    private void EndPour()
    {
        currentStream.End();
        currentStream = null;

    }

    private float CalculatePourAngle()
    {
        //foward or up, i think up good
        return transform.up.y * Mathf.Rad2Deg;

    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform );
        return streamObject.GetComponent<Stream>();
   
    }

 
}
