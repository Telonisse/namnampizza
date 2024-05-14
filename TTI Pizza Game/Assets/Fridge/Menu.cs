using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
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
