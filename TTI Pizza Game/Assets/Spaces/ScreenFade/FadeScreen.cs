using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] Color fadeColor;
    [SerializeField] float fadeDuration;
    [SerializeField] bool fadeOnStart;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }
    public void FadeIn()
    {
        Fade(1, 0);
    }
    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
        fadeOnStart = false;
    }

    private IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newcolor = fadeColor;
            newcolor.a = Mathf.Lerp(alphaIn, alphaOut, timer/fadeDuration);
            rend.material.SetColor("_Color", newcolor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newcolor2 = fadeColor;
        newcolor2.a = alphaOut;
        rend.material.SetColor("_Color", newcolor2);
    }
}