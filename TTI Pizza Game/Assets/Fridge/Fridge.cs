using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct toppingInFridge
{
    public string toppingTag;
    public GameObject toppingGameObject;
    public bool isInFridge;
    public GameObject objectSpawnPos;
}

public class Fridge : MonoBehaviour
{
    [SerializeField] toppingInFridge[] toppingInFridgeArray;
    private HingeJoint joint;
    [SerializeField] bool open;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
    }
    private void Update()
    {
        bool isDone = false;
        if (!open)
        {
            Collider[] colliders = Physics.OverlapBox(new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z), new Vector3(1 / 2 , 2 / 2, 1 / 2), Quaternion.identity);
            for (int i = 0; i < colliders.Length; i++)
            {
                for (int j = 0; j < toppingInFridgeArray.Length; j++)
                {
                    if (colliders[i].tag == toppingInFridgeArray[j].toppingTag)
                    {
                        toppingInFridgeArray[j].isInFridge = true;
                    }
                }
                isDone = true;
            }
        }
        if (!open && isDone)
        {
            for (int i = 0; i < toppingInFridgeArray.Length; i++)
            {
                if (toppingInFridgeArray[i].isInFridge == false)
                {
                    Instantiate(toppingInFridgeArray[i].toppingGameObject, toppingInFridgeArray[i].objectSpawnPos.transform.TransformPoint(toppingInFridgeArray[i].objectSpawnPos.transform.localPosition), toppingInFridgeArray[i].objectSpawnPos.transform.rotation);
                    toppingInFridgeArray[i].isInFridge = true;
                }
            }
        }
    }
    public void OpenFridge()
    {
        var motor = joint.motor;
        if (!open)
        {
            motor.targetVelocity = -200;
            joint.motor = motor;
        }
        if (open)
        {
            motor.targetVelocity = 200;
            joint.motor = motor;
        }
    }

    public void CloseFridge()
    {
        if (open)
        {
            for(int i = 0;i < toppingInFridgeArray.Length; i++)
            {
                toppingInFridgeArray[i].isInFridge = false;
            }
            open = false;
        }
        else
        {
            open = true;
        }
    }
    void OnDrawGizmos()
    {
        // Define the parameters for the overlap box
        Vector3 center = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);
        Vector3 size = new Vector3(1, 2, 1);

        // Draw the overlap box wireframe using Gizmos
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size);
    }
}
