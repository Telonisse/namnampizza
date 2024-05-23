using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeleteParent : MonoBehaviour
{


    public GameObject myGameObject;

    private void Update()
    {
        DeleteObject(myGameObject);
    }

    public void DeleteObject(GameObject obj)
    {
        if (obj.transform.childCount == 0)
        {
            Destroy(obj);
        }
      
    }

}
