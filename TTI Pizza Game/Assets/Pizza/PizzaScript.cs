using Oculus.Interaction;
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
    [SerializeField] GameObject tomatoSauce;
    [SerializeField] GameObject cheese;

    public bool cheeseOnPizza = false;
    public bool tomatoSauceOnPizza = false;

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

        if (cheese.transform.lossyScale.x >= 0.24f)
        {
            cheeseOnPizza = true;
        }
        if (tomatoSauce.transform.lossyScale.x >= 0.26f)
        {
            tomatoSauceOnPizza = true;
        }
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
                BoxCollider box = other.GetComponent<BoxCollider>();
                other.transform.position = new Vector3(other.transform.position.x, transform.parent.position.y - box.center.y + 0.02f, other.transform.position.z);
                other.transform.SetParent(transform);

                other.gameObject.GetComponent<Grabbable>().enabled = false;
                other.gameObject.GetComponent<PhysicsGrabbable>().enabled = false;
                other.gameObject.GetComponent<TouchHandGrabInteractable>().enabled = false;
                other.gameObject.GetComponent<MeshCollider>().enabled = false;
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;

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
                tomatoSauce.transform.GetComponent<IngredientScript>().ChangeMaterialDone();
                cheese.transform.GetComponent<IngredientScript>().ChangeMaterialDone();

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
                tomatoSauce.transform.GetComponent<IngredientScript>().ChangeMaterialBurned();
                cheese.transform.GetComponent<IngredientScript>().ChangeMaterialBurned();

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

    public void GetToppings(int index, out bool isOnPizza, out bool tomatoSauce, out bool cheese)
    {
        isOnPizza = pizzaToppingsArray[index].isOnPizza;
        tomatoSauce = tomatoSauceOnPizza;
        cheese = cheeseOnPizza;
    }
}
