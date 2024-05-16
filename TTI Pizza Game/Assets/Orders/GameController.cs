using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int[] pointLevel;

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

    }
    private void Update()
    {
        if (pointLevel[currentLevel - 1] == 30)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    public void Points(int points)
    {
        pointLevel[currentLevel - 1] += points;
    }
    public int CurrentPoints()
    {
        return pointLevel[currentLevel - 1];
    }
}
