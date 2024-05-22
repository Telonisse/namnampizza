using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int[] pointLevel;

    private Vector3 fridgePos;
    private Vector3 ovenPos;
    private Vector3 luckaPos;
    private Vector3 doorPos;

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
            SceneManager.LoadSceneAsync(1);
            pointLevel[currentLevel - 1] = 0;
        }
    }

    private IEnumerator WinSequence()
    {
        fade.FadeOut(true);
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
    public void SavePos(Vector3 fridgepos1, Vector3 ovenpos1, Vector3 luckapos1, Vector3 doorpos1)
    {
        fridgePos = fridgepos1;
        ovenPos = ovenpos1;
        luckaPos = luckapos1;
        doorPos = doorpos1;
    }
    public void GetPos(out Vector3 fridgepos1,out Vector3 ovenpos1,out Vector3 luckapos1, out Vector3 doorpos1)
    {
        fridgepos1 = fridgePos;
        ovenpos1 = ovenPos;
        luckapos1 = luckaPos;
        doorpos1 = doorPos;
    }
}
