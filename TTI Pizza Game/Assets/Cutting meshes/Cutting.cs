using Meta.XR.Editor.Tags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    // gameObject.tag = "SomeTagName";

    public Transform myChildObjectPart1;
    public Transform myChildObjectPart2;
    public Transform myChildObjectPart3;

    public bool detachChildPart1;
    public bool detachChildPart2;
    public bool detachChildPart3;

    //public string collisionTag = "Sausage";

    public bool knifeHasTouched = false;

    public bool collidedWithPart1 = false;
    public bool collidedWithPart2 = false;
    public bool collidedWithPart3 = false;


    void Update()
    {
        if (detachChildPart1 == true)
        {
            myChildObjectPart1.parent = null;
        }
        if (detachChildPart2 == true)
        {
            myChildObjectPart2.parent = null;
        }
        if (detachChildPart3 == true)
        {
            myChildObjectPart3.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            knifeHasTouched = true;
            Debug.Log("touchedKnife");
        }
        else if (other.gameObject.CompareTag("Part1"))
        {
            collidedWithPart1 = true;
            Debug.Log("touchedpart1");
        }
        else if (other.gameObject.CompareTag("Part2"))
        {
            collidedWithPart2 = true;
            Debug.Log("touchedpart2");
        }
        else if (other.gameObject.CompareTag("Part3"))
        {
            collidedWithPart3 = true;
            Debug.Log("touchedpart3");
        }

        if (knifeHasTouched && collidedWithPart1)
        {
            detachChildPart1 = true;
        }
        if(knifeHasTouched && collidedWithPart2)
        {
            detachChildPart2 = true;
        }
        if(knifeHasTouched && collidedWithPart3)
        {
            detachChildPart3 = true;
        }

    }
}
