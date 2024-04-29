using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseGrater : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    [SerializeField] private Animator myAnimationController;
    public string collisionTag = "Tomatosauce";


    private bool IsGrating = false;
    private Stream currentStream = null;

    private void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if (IsGrating != pourCheck)
        {
            IsGrating = pourCheck;

            if (IsGrating)
            {
                StartGrating();
            }
            else
            {
                EndGrating();
                myAnimationController.SetFloat("PourValue", 0.0f);
            }
        }

        if (IsGrating)
        {
            RaycastHit hit;
            if (Physics.Raycast(origin.position, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(collisionTag))
                {
                    Debug.Log("Collision detected with tag: " + collisionTag);
                    myAnimationController.SetBool("IsPouring", true);
                    myAnimationController.SetFloat("PourValue", 1.0f);


                }
            }
        }
    }

    private void StartGrating()
    {
        currentStream = CreateStream();
        currentStream.Begin();

    }

    private void EndGrating()
    {
        currentStream.End();
        currentStream = null;

    }

    // I dont think i need 
    private float CalculatePourAngle()
    {
        //foward or up, i think up good
        return transform.up.y * Mathf.Rad2Deg;

    }

    //Need it to create the particlesystem
    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();

    }
}
