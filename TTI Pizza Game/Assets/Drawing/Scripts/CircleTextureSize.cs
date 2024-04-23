using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTextureSize : MonoBehaviour
{
    // The radius of the circle
    public float radius = 1.0f;

    // The resolution of the circle texture
    public int textureResolution = 512;

    void Start()
    {
        // Create a new texture to represent the circle
        Texture2D circleTexture = CreateCircleTexture(radius, textureResolution);

        // Get the width and height of the texture
        int textureWidth = circleTexture.width;
        int textureHeight = circleTexture.height;

        // Print the dimensions to the console
        Debug.Log("Circle Texture Size: " + textureWidth + "x" + textureHeight);
    }

    Texture2D CreateCircleTexture(float radius, int resolution)
    {
        // Create a new texture
        Texture2D texture = new Texture2D(resolution, resolution);

        // Calculate the center position
        Vector2 center = new Vector2(resolution / 2f, resolution / 2f);

        // Loop through all pixels in the texture
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // Calculate the distance from the current pixel to the center
                float distance = Vector2.Distance(new Vector2(x, y), center);

                // Set the pixel color based on whether it's inside the circle
                Color color = (distance <= radius) ? Color.white : Color.clear;
                texture.SetPixel(x, y, color);
            }
        }

        // Apply changes and return the texture
        texture.Apply();
        return texture;
    }
}
