using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshPro levelText;
    [SerializeField] TextMeshPro pointsText;
    private int currentLevel = 1;
    [SerializeField] int numOfLevels;
    GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        levelText.text = "Level " + currentLevel.ToString();
        pointsText.text = gameController.LevelPoints(currentLevel).ToString();
    }

    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel > numOfLevels)
        {
            currentLevel = 1;
        }
        levelText.text = "Level " + currentLevel.ToString();
        pointsText.text = gameController.LevelPoints(currentLevel).ToString();
    }
    public void PrevLevel()
    { 
        currentLevel--;
        if (currentLevel < numOfLevels)
        {
            currentLevel = numOfLevels;
        }
        levelText.text = "Level " + currentLevel.ToString();
        pointsText.text = gameController.LevelPoints(currentLevel).ToString();
    }
    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
