using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStarter : MonoBehaviour
{
    LevelPicker levelPicker;
    [SerializeField] TextMeshPro whatLevel;
    private void Start()
    {
        levelPicker = FindObjectOfType<LevelPicker>(); 
    }

    private void Update()
    {

        if (levelPicker.whichLevel == 2) 
        {
            whatLevel.text = "1";
        }
        if (levelPicker.whichLevel == 3)
        {
            whatLevel.text = "2";
        }
        if (levelPicker.whichLevel == 4)
        {
            whatLevel.text = "3";
        }
        if (levelPicker.whichLevel == 5)
        {
            whatLevel.text = "4";
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stamp"))
        {
            SceneManager.LoadSceneAsync(levelPicker.whichLevel);
            Debug.Log("Meow" +  levelPicker.whichLevel);

        }
    }
}
