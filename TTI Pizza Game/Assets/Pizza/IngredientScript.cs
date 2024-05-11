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

    [SerializeField] Material myTomatoSauceMaterial;
    [SerializeField] Texture rawTextureTomatoSauce;
    [SerializeField] Texture burnedTextureTomatoSauce;

    [SerializeField] Material myGratedCheeseMaterial;
    [SerializeField] Texture rawTextureGratedCheese;
    [SerializeField] Texture doneTextureGratedCheese;
    [SerializeField] Texture burnedTextureGratedCheese;

    private void Start()
    {
        material.mainTexture = rawTexture;
        myTomatoSauceMaterial.mainTexture = rawTextureTomatoSauce;
        myGratedCheeseMaterial.mainTexture = rawTextureGratedCheese;

    }

    public void ChangeMaterialDone()
    {
        material.mainTexture = doneTexture;
        myGratedCheeseMaterial.mainTexture = doneTextureGratedCheese;


    }
    public void ChangeMaterialBurned()
    {
        material.mainTexture = burnedTexture;
        myTomatoSauceMaterial.mainTexture = burnedTextureTomatoSauce;
        myGratedCheeseMaterial.mainTexture = burnedTextureGratedCheese;
    }
}
