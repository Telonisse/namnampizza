using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarPoints : MonoBehaviour
{
    [SerializeField] private GameObject[] starEmpty; 
    [SerializeField] private GameObject[] starFull;  
    [SerializeField] private int level; // Level for script specficcicicopkokcciko

    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();

        if (gameController != null)
        {
            UpdateStars();
        }
    }

    private void UpdateStars()
    {
        int points = gameController.GetPointLevel(level); 
        int savedStars = PlayerPrefs.GetInt("Stars_Level_" + level, 0); 

        foreach (var star in starFull)
        {
            star.SetActive(false);
        }

        foreach (var star in starEmpty)
        {
            star.SetActive(true);
        }

        for (int i = 0; i < savedStars; i++)
        {
            starFull[i].SetActive(true);
            if (i < starEmpty.Length)
            {
                starEmpty[i].SetActive(false);
            }
        }

        int starsEarned = StarCalculator(points);
        for (int i = 0; i < starsEarned; i++)
        {
            starFull[i].SetActive(true);
            if (i < starEmpty.Length)
            {
                starEmpty[i].SetActive(false);
            }
        }

        PlayerPrefs.SetInt("Stars_Level_" + level, starsEarned);
        PlayerPrefs.Save();
    }

    private int StarCalculator(int points)
    {
        if (points >= 100)
        {
            return 3;
        }
        else if (points >= 50)
        {
            return 2;
        }
        else if (points >= 10)
        {
            if (level == 0)
            {
                return 3;
            }
            return 1;

    
        }
        else
        {
            return 0;
        }
    }
}
