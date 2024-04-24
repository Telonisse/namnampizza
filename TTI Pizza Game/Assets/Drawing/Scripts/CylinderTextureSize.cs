using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderTextureSize : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            Material material = renderer.material;

            if (material != null)
            {
                // Get the mesh bounds of the cylinder
                Bounds bounds = CalculateMeshBounds();

                // Calculate the diameter of the cylinder
                float diameter = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);

                // Set the texture size to match the diameter
                material.mainTextureScale = new Vector2(diameter, diameter);

                Debug.Log(gameObject.name + " Texture Size set to: " + diameter + "x" + diameter);
            }
            else
            {
                Debug.LogError("Material of " + gameObject.name + " is missing!");
            }
        }
        else
        {
            Debug.LogError("Renderer component of " + gameObject.name + " is missing!");
        }
    }

    Bounds CalculateMeshBounds()
    {
        // Get the mesh filter attached to the cylinder
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            // Return the bounds of the mesh
            return meshFilter.sharedMesh.bounds;
        }
        else
        {
            Debug.LogError("Mesh filter or shared mesh of " + gameObject.name + " is missing!");
            return new Bounds(Vector3.zero, Vector3.zero);
        }
    }
}


