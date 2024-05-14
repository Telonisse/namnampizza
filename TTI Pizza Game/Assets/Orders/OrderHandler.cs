using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderHandler : MonoBehaviour
{
    [SerializeField] Order[] orders;
    [SerializeField] int numOfToppings;
    [SerializeField] int pointsGoodPizza;
    [SerializeField] int pointsBadPizza;
    [SerializeField] TextMeshPro pointsText;

    private bool isOnPizza;
    private bool isOnOrder;
    private bool matchesOrder = false;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        pointsText.text = gameController.CurrentPoints().ToString();
        Debug.Log(gameController.CurrentPoints());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null )
        {
            PizzaScript pizza = other.GetComponent<PizzaScript>();
            for (int i = 0; i < orders.Length; i++)
            {
                matchesOrder = true;
                if (orders[i].IsOffline() == false && other != null && pizza != null)
                {
                    for (int j = 0; j < numOfToppings; j++)
                    {
                        pizza.GetToppings(j, out isOnPizza, out bool tomatoSauce, out bool cheese);
                        orders[i].GetToppings(j, out isOnOrder);
                        if (isOnOrder != isOnPizza || !tomatoSauce || !cheese)
                        {
                            matchesOrder = false;
                        }
                    }
                    if (matchesOrder && other != null && pizza != null)
                    {
                        orders[i].ResetOrder();
                        Debug.Log(other.name);
                        Destroy(other.transform.parent.parent.gameObject);
                        gameController.Points(pointsGoodPizza);
                        pointsText.text = gameController.CurrentPoints().ToString();
                        other = null;
                    }
                }
            }
            if (!matchesOrder && pizza != null)
            {
                Debug.Log(other.name);
                gameController.Points(pointsBadPizza);
                pointsText.text = gameController.CurrentPoints().ToString();
            }
        }
    }
}
