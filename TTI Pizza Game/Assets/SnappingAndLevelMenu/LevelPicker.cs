using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPicker : MonoBehaviour
{
    public int whichLevel = 0;

    // fix numbers for  right scene 
    public void PickLevel1()
    {
        whichLevel = 1;
    }
    public void PickLevel2()
    {
        whichLevel = 2;
    }
    public void PickLevel3()
    {
        whichLevel = 3;
    }
    public void PickLevel4()
    {
         whichLevel = 5;
    }
}
