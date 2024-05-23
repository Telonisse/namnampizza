using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] Color fadeColor;
    [SerializeField] float fadeDuration;
    [SerializeField] bool fadeOnStart;
    [SerializeField] GameObject text;
    [SerializeField] TextMeshPro pointText;

    private Renderer rend;
    private GameController gameController;

    void Start()
    {
        rend = GetComponent<Renderer>();
        gameController = FindObjectOfType<GameController>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }
    public void FadeIn()
    {
        Fade(1, 0);
    }
    public void FadeOut(bool showText)
    {
        if (showText )
        {
            text.SetActive(true);
        }
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
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

    private void Update()
    {
        if (gameController != null)
        {
            pointText.text = gameController.CurrentPoints().ToString();
        }
    }
}
