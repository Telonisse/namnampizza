using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct pizzaToppings
{
    public string topping;
    public bool isOnPizza;
}

public class PizzaScript : MonoBehaviour
{
    [SerializeField] pizzaToppings[] pizzaToppingsArray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oven"))
        {

        }
        for (int i = 0; i < pizzaToppingsArray.Length; i++)
        {
            if (pizzaToppingsArray[i].topping == other.tag)
            {
                pizzaToppingsArray[i].isOnPizza = true;
                other.transform.SetParent(transform);
                other.GetComponent<Rigidbody>().isKinematic = true;
                //so object no go stretch
                other.transform.localScale = new Vector3(0.1f, 10, 0.1f); 
                other.transform.rotation = Quaternion.identity;
            }
        }
    }
}
