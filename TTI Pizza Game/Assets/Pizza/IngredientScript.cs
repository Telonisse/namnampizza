using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientScript : MonoBehaviour
{
    [SerializeField] Material doneMaterial;
    [SerializeField] Material burnedMaterial;

    private MeshRenderer render;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();

        if (render = null)
        {
            Debug.LogWarning("Renderer not found");
        }
    }

    public void ChangeMaterialDone()
    {
        if (doneMaterial != null)
        {
            render.material = doneMaterial;
        }
    }
    public void ChangeMaterialBurned()
    {
        if(burnedMaterial != null)
        {
            render.material = burnedMaterial;
        }
    }
}
