using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza") && other.name != "Pizza" && other.name != "PizzaScript")
        {
            Debug.Log(other.name);
            Counters counters = FindObjectOfType<Counters>();
            Vector3 pos = new Vector3(counters.transform.position.x, counters.transform.position.y + 1.2f, counters.transform.position.z);
            other.transform.rotation.Set(0, 0, 0, 0);
            other.transform.position = pos;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
