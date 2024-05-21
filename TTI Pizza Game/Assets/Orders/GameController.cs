using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int[] pointLevel;
    [SerializeField] GameObject textCompleted;

    private FadeScreen fade;
    int currentLevel = 1;
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameController>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        fade = FindObjectOfType<FadeScreen>();
    }
    private void Update()
    {
        if (pointLevel[currentLevel - 1] == 30)
        {
            StartCoroutine(WinSequence());
            SceneManager.LoadSceneAsync(0);
            pointLevel[currentLevel - 1] = 0;
        }
    }

    private IEnumerator WinSequence()
    {
        textCompleted.SetActive(true);
        fade.FadeOut();
        yield return new WaitForSecondsRealtime(2);
    }

    public void Points(int points)
    {
        pointLevel[currentLevel - 1] += points;
    }
    public int CurrentPoints()
    {
        return pointLevel[currentLevel - 1];
    }
    public int LevelPoints(int level)
    {
        return pointLevel[level];
    }
}
