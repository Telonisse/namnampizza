using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin = null;
    public GameObject streamPrefab= null;

    private bool IsPouring = false;
    private Stream currentStream = null;

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
