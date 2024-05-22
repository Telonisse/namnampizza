using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    private void Start()
    {
    }
    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
