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

    //[SerializeField] Material myTomatoSauceMaterial;
    //[SerializeField] Texture rawTextureTomatoSauce;
    //[SerializeField] Texture doneTextureTomatoSauce;
    //[SerializeField] Texture burnedTextureTomatoSauce;

    private void Start()
    {
        material.mainTexture = rawTexture;
       // myTomatoSauceMaterial.mainTexture = rawTextureTomatoSauce;

    }

    public void ChangeMaterialDone()
    {
        material.mainTexture = doneTexture;
        //myTomatoSauceMaterial.mainTexture = doneTextureTomatoSauce;

    }
    public void ChangeMaterialBurned()
    {
        material.mainTexture = burnedTexture;
        //myTomatoSauceMaterial.mainTexture = doneTextureTomatoSauce;

    }
}
