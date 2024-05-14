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
    private bool movedDone = true;

    private float timer = 0;
    private bool isRunning = false;

    private void Start()
    {
        joint = GetComponent<HingeJoint>();
    }
    private void Update()
    {
        if (movedDone == true)
        {
            bool isDone = false;
            if (!open)
            {
                Collider[] colliders = Physics.OverlapBox(new Vector3(transform.parent.position.x, transform.parent.position.y + 1, transform.parent.position.z), new Vector3(0.7f / 2, 1.2f / 2, 0.7f / 2), Quaternion.identity);
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

            if (isRunning)
            {
                timer += Time.deltaTime;
            }
            if (timer < 2)
            {
                isRunning = false;
            }

            if (!open && isDone && !isRunning)
            {
                for (int i = 0; i < toppingInFridgeArray.Length; i++)
                {
                    if (toppingInFridgeArray[i].isInFridge == false)
                    {
                        Instantiate(toppingInFridgeArray[i].toppingGameObject, toppingInFridgeArray[i].objectSpawnPos.transform.position, toppingInFridgeArray[i].toppingGameObject.transform.rotation);
                        toppingInFridgeArray[i].isInFridge = true;
                    }
                }
            }
        }
    }
    public void MovedDone()
    {
        movedDone = true;
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
            isRunning = true;
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
        Vector3 center = new Vector3(transform.parent.position.x, transform.parent.position.y + 1, transform.parent.position.z);
        Vector3 size = new Vector3(0.7f, 1.2f, 0.7f);

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(center, transform.parent.rotation, size);
        Gizmos.matrix = rotationMatrix;

        // Draw the overlap box wireframe using Gizmos
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
