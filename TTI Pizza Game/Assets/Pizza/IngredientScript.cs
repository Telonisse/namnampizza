using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    [SerializeField] GameObject raw;
    [SerializeField] GameObject cooked;
    [SerializeField] GameObject burned;

    private void Start()
    {
        raw.SetActive(true);
        cooked.SetActive(false);
        burned.SetActive(false);
    }

    public void ChangeMaterialDone()
    {
        raw.SetActive(false);
        cooked.SetActive(true);
        burned.SetActive(false);
    }
    public void ChangeMaterialBurned()
    {
        raw.SetActive(false);
        cooked.SetActive(false);
        burned.SetActive(true);
    }
}
