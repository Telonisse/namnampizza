using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHandler : MonoBehaviour
{
    [SerializeField] Order[] orders;
    [SerializeField] int numOfToppings;

    private bool isOnPizza;
    private bool isOnOrder;
    private bool matchesOrder = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null )
        {
            PizzaScript pizza = other.GetComponentInChildren<PizzaScript>();
            for (int i = 0; i < orders.Length; i++)
            {
                matchesOrder = true;
                if (orders[i].IsOffline() == false && other != null)
                {
                    for (int j = 0; j < numOfToppings; j++)
                    {
                        pizza.GetToppings(j, out isOnPizza);
                        orders[i].GetToppings(j, out isOnOrder);
                        if (isOnOrder != isOnPizza)
                        {
                            matchesOrder = false;
                        }
                    }
                    if (matchesOrder && other != null)
                    {
                        orders[i].ResetOrder();
                        Debug.Log(other.name);
                        Destroy(other.transform.parent.gameObject);
                        other = null;
                    }
                }
            }
        }
    }
}
