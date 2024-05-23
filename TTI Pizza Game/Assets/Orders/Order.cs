using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct orderToppings
{
    public string topping;
    public bool onOrder;
}

public class Order : MonoBehaviour
{
    [SerializeField] orderToppings[] orderArray;

    private int randomTopping;
    [SerializeField] TextMeshPro textObjectOrder;
    [SerializeField] GameObject graphics;
    [SerializeField] int maxToppingPerOrder;
    private string orderText;

    //Timer
    private float timer = 0f;
    private bool isRunning = false;
    [SerializeField] float maxTimer;

    bool offline = false;

    private void Start()
    {
        NewOrder(maxToppingPerOrder);
    }
    private void Update()
    {
        if (isRunning == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= maxTimer)
        {
            ResetOrder();
            Debug.Log("Timer Stopped");
            isRunning = false;
            timer = 0f;
            offline = false;
            graphics.SetActive(true);
            NewOrder(maxToppingPerOrder);
        }
    }
    public void NewOrder(int maxToppings)
    {

        orderText = null;
        orderText += "Tomato sauce";
        orderText += "\n";
        orderText += "Cheese";
        orderText += "\n";

        for (int i = 0; i < maxToppings; i++)
        {
            randomTopping = Random.Range(0, orderArray.Length);
            while (orderArray[randomTopping].onOrder == true)
            {
                randomTopping = Random.Range(0, orderArray.Length);
            }
            orderArray[randomTopping].onOrder = true;
        }
        foreach (orderToppings order in orderArray)
        {
            if (order.onOrder == true)
            {
                orderText += order.topping;
                orderText += "\n";
            }
        }
        textObjectOrder.text = orderText;
    }
    public void ResetOrder()
    {
        for(int i = 0;i < orderArray.Length; i++)
        {
            graphics.SetActive(false);
            offline = true;
            orderArray[i].onOrder = false;
            isRunning = true;
        }
        maxTimer = 15f;
    }
    public bool IsOffline()
    {
        if (offline == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetToppings(int index, out bool isOnOrder)
    {
        isOnOrder = orderArray[index].onOrder;
    }
}
