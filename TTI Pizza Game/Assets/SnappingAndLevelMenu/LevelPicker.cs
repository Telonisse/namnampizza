using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPicker : MonoBehaviour
{
    public int whichLevel = 0;

    public bool level1 = false;
    public bool level2 = false;
    public bool level3 = false;
    public bool level4 = false;

     void Update()
    {
        if (level1 == true)
        {
            whichLevel = 1;
            Debug.Log(whichLevel);
        }
        if (level2 == true)
        {
            whichLevel = 2;
            Debug.Log(whichLevel);
        }
        if (level3 == true)
        {
            whichLevel = 3;
            Debug.Log(whichLevel);
        }
        if (level4 == true)
        {
            whichLevel = 4;
            Debug.Log(whichLevel);
        }

    }
    public void PickLevel1()
    {
        level1 = true;
        level2 = false;
        level3 = false;
        level4 = false;
        //whichLevel = 2;
    }
    public void PickLevel2()
    {
        level1 = false;
        level2 = true;
        level3 = false;
        level4 = false;

        //whichLevel = 3;
    }
    public void PickLevel3()
    {
        level1 = false;
        level2 = false;
        level3 = true;
        level4 = false;
        //whichLevel = 4;
    }
    public void PickLevel4()
    {
        level1 = false;
        level2 = false;
        level3 = false;
        level4 = true;
        //whichLevel = 5;
    }


}
