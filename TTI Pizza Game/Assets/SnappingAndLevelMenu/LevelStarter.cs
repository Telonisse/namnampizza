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
        levelPicker = FindObjectOfType<LevelPicker>(); // Finds the LevelPicker in the scene
        
        //RAWR I DONT LIKE THIS SHIT GRR GRRGRGRHGAAAAARGH
    }

    private void Update()
    {
        // whatLevel.text = "Level Selected: " + levelPicker.whichLevel.ToString();

        if (levelPicker.level1 == true)
        {
            whatLevel.text = "1";
            Debug.Log("i am true 1");
        }

        if (levelPicker.level2 == true)
        {
            whatLevel.text = "2";
        }

        if (levelPicker.level3 == true)
        {
            whatLevel.text = "3";
        }

        if (levelPicker.level4 == true)
        {
            whatLevel.text = "4";
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            //SceneManager.LoadSceneAsync(levelPicker.whichLevel);
            Debug.Log(levelPicker.whichLevel);

        }
    }
}
