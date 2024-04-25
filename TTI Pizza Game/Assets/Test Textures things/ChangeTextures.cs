using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextures : MonoBehaviour
{
    public Material material;
    public Texture rawPizza;
    public Texture burnedPizza;

    public bool myBool;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myBool = !myBool;

            if (myBool)
            {
                //Display Texture 1
                material.mainTexture = rawPizza;
            }
            else
            {
                //Display Texture 2
                material.mainTexture = burnedPizza;

            }
        }
    }
}
