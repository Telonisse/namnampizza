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
    [SerializeField] bool open;
    private bool movedDone = false;

    private void Start()
    {
    }
    private void Update()
    {
        if (movedDone == true)
        {
            bool isDone = false;
            if (!open)
            {
                Collider[] colliders = Physics.OverlapBox(new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y + 1, transform.parent.parent.position.z), new Vector3(0.7f / 2, 1.2f / 2, 0.7f / 2), Quaternion.identity);
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
                        Instantiate(toppingInFridgeArray[i].toppingGameObject, toppingInFridgeArray[i].objectSpawnPos.transform.position, toppingInFridgeArray[i].toppingGameObject.transform.rotation);
                        toppingInFridgeArray[i].isInFridge = true;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fridge"))
        {
            Debug.Log("Closed");
            open = false;
            for (int i = 0; i < toppingInFridgeArray.Length; i++)
            {
                toppingInFridgeArray[i].isInFridge = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Fridge"))
        {
            Debug.Log("Open");
            open = true;
        }
    }
    public void MovedDone()
    {
        movedDone = true;
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
