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

    //Timer in oven
    [SerializeField] float minTimeDone = 0;
    [SerializeField] float minTimeBurned = 0;

    private float timer = 0;
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
            timer = 0f;
            isRunning = true;
        }
        for (int i = 0; i < pizzaToppingsArray.Length; i++)
        {
            if (pizzaToppingsArray[i].topping == other.tag)
            {
                other.GetComponent<Rigidbody>().isKinematic = true;
                pizzaToppingsArray[i].isOnPizza = true;
                other.transform.position = new Vector3(other.transform.position.x, transform.parent.position.y + 0.05f, other.transform.position.z); //change 0.05f to whatever half of the scale is cus that shit isnt fucking working and idk why
                other.transform.SetParent(transform);

                //so object no go stretch
                other.transform.localScale = new Vector3(0.1f, 10, 0.1f); 
                other.transform.rotation = Quaternion.identity;
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
                Debug.Log("undone");
                break;
            case pizzaStates.done:
                for (int i = 0; i < transform.childCount; i++)
                {
                    //transform.GetChild(i).GetComponent<IngredientScript>().ChangeMaterialDone();
                }
                break;
            case pizzaStates.burned:
                for (int i = 0; i < transform.childCount; i++)
                {
                    //transform.GetChild(i).GetComponent<IngredientScript>().ChangeMaterialBurned();
                }
                break;
            default:
                break;

        }
    }
}
