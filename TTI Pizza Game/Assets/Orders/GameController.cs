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

    public FadeScreen fade;
    public int currentLevel = 1;

    private float timer = 0f;
    [SerializeField] bool usesTimer = false;
    [SerializeField] float maxTimer;
    bool win = false;
    public GameObject timerObject;
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
        timerObject = GameObject.Find("Timer");
        timerObject.SetActive(false);
    }
    private void Update()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex - 2;
        if (currentLevel < 0)
        {
            currentLevel = 0;
        }

        if (fade == null)
        {
            fade = FindObjectOfType<FadeScreen>();
            win = false;
        }
        if (timerObject == null)
        {
            timerObject = GameObject.Find("Timer");
            timerObject.SetActive(false);
        }
        if (currentLevel == 0 && SceneManager.GetActiveScene().buildIndex == 2 && pointLevel[currentLevel] >= 1 && !win)
        {
            win = true;
            StartCoroutine(WinSequence());
        }
        if (currentLevel > 0)
        {
            usesTimer = true;
            maxTimer = 600;
        }
        else
        {
            usesTimer = false;
            timer = 0f;
            maxTimer = 1f;
        }
        if (usesTimer == true)
        {
            timer += Time.deltaTime;
        }
        if (timer >= 590 && timer <= 591)
        {
            timerObject.SetActive(true);
        }
        if (timer >= maxTimer && !win)
        {
            win = true;
            Debug.Log("Timer Stopped");
            timer = 0f;
            StartCoroutine(WinSequence());
        }
    }

    private IEnumerator WinSequence()
    {
        fade.FadeOut(false);
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadSceneAsync(1);
    }

    public void Points(int points)
    {
        pointLevel[currentLevel] += points;
    }
    public int CurrentPoints()
    {
        return pointLevel[currentLevel];
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



    // DONT KILL ME THIS ALL I ADDED I PROMISE
    public int GetPointLevel(int level)
    {
        if (level >= 0 && level < pointLevel.Length)
        {
            return pointLevel[level];
        }
        return 0;
    }

}
