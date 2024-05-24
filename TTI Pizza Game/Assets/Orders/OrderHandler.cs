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
    [SerializeField] ParticleSystem confetti;

    private bool isOnPizza;
    private bool isOnOrder;
    private bool matchesOrder = false;
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        pointsText.text = gameController.CurrentPoints().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other != null )
        {
            Debug.Log("not null");
            PizzaScript pizza = other.GetComponent<PizzaScript>();
            for (int i = 0; i < orders.Length; i++)
            {
                matchesOrder = true;
                if (orders[i].IsOffline() == false && other != null && pizza != null)
                {
                    Debug.Log("pizza not null");
                    for (int j = 0; j < numOfToppings; j++)
                    {
                        pizza.GetToppings(j, out isOnPizza, out bool tomatoSauce, out bool cheese, out bool state);
                        orders[i].GetToppings(j, out isOnOrder);
                        if (isOnOrder != isOnPizza || !tomatoSauce || !cheese || !state)
                        {
                            matchesOrder = false;
                            Debug.Log("Not Matching");
                        }
                    }
                    if (matchesOrder && other != null && pizza != null)
                    {
                        orders[i].ResetOrder();
                        confetti.Play();
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
                Destroy(other.transform.parent.parent.gameObject);
            }
        }
    }
}
