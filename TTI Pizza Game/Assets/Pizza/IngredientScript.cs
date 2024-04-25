using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] Texture rawTexture;
    [SerializeField] Texture doneTexture;
    [SerializeField] Texture burnedTexture;

    private void Start()
    {
        material.mainTexture = rawTexture;
    }

    private void Update()
    {

    }

    public void ChangeMaterialDone()
    {
        material.mainTexture = doneTexture;
    }
    public void ChangeMaterialBurned()
    {
        material.mainTexture = burnedTexture;
    }
}
