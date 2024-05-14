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
    [SerializeField] GameObject pizzaObject;
    

    //Timer in oven
    [SerializeField] float minTimeDone = 0;
    [SerializeField] float minTimeBurned = 0;

    float timeInOven;
    float maxTimeInOven;
    float ratio;

    public float timer = 0;
    private bool isRunning = false;

    private enum pizzaStates
    {
        uncooked,
        done,
        burned
    }

    [SerializeField] pizzaStates currentState;

    private void Update()
    {
        ratio = timeInOven / maxTimeInOven;
        ratio = Mathf.Clamp01(ratio);


        if (isRunning)
        {
            timer += Time.deltaTime;
        }
        if (timer < minTimeDone)
        {
            ChangeState(pizzaStates.uncooked);
        }
        if (timer >= minTimeDone && timer < minTimeBurned)
        {
            ChangeState(pizzaStates.done);
        }
        if (timer >= minTimeBurned)
        {
            ChangeState(pizzaStates.burned);
            isRunning = false;
        }
        StateActions();
    }
    
    private void ChangeState(pizzaStates state)
    {
        currentState = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oven"))
        {
            isRunning = true;
        }
        for (int i = 0; i < pizzaToppingsArray.Length; i++)
        {
            if (pizzaToppingsArray[i].topping == other.tag)
            {
                other.GetComponent<Rigidbody>().isKinematic = true;
                pizzaToppingsArray[i].isOnPizza = true;
                other.transform.position = new Vector3(other.transform.position.x, transform.parent.position.y + 0.01f, other.transform.position.z); //change 0.05f to whatever half of the scale is cus that shit isnt fucking working and idk why
                other.transform.SetParent(transform);

                transform.parent.rotation = Quaternion.identity;
                other.transform.rotation = Quaternion.identity;
                //other.transform.localScale = new Vector3(1,1,1); pls scale work
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Oven"))
        {
            isRunning = false;
        }
    }

    private void StateActions()
    {
        switch (currentState)
        {
            case pizzaStates.uncooked:
                break;
            case pizzaStates.done:
                pizzaObject.transform.GetComponent<IngredientScript>().ChangeMaterialDone();
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<IngredientScript>() != null)
                    {
                        transform.GetChild(i).GetComponent<IngredientScript>().ChangeMaterialDone();
                    }
                }
                break;
            case pizzaStates.burned:
                pizzaObject.transform.GetComponent<IngredientScript>().ChangeMaterialBurned();

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<IngredientScript>() != null)
                    {
                        transform.GetChild(i).GetComponent<IngredientScript>().ChangeMaterialBurned();
                    }
                }
                break;
            default:
                break;

        }
    }

    public void GetToppings(int index, out bool isOnPizza)
    {
        isOnPizza = pizzaToppingsArray[index].isOnPizza;
    }
}
