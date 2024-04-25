using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSauceParticle : MonoBehaviour
{
    public Texture2D textureMask;
    public float maskScale = 1f;

    [System.Obsolete]
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Whiteboard"))
        {
            ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[1];
            int numCollisionEvents = GetComponent<ParticleSystem>().GetCollisionEvents(other, collisionEvents);
            if (numCollisionEvents > 0)
            {
                Vector2 uvCoords = collisionEvents[0].intersection / maskScale;

                UpdateTextureMask(uvCoords);
            }
        }
    }

    private void UpdateTextureMask(Vector2 uvCoords)
    {
        uvCoords.x = Mathf.Clamp01(uvCoords.x);
        uvCoords.y = Mathf.Clamp01(uvCoords.y);

        int x = Mathf.FloorToInt(uvCoords.x * textureMask.width);
        int y = Mathf.FloorToInt(uvCoords.y * textureMask.height);

        textureMask.SetPixel(x, y, Color.white);
        textureMask.Apply(); 
    }
}
