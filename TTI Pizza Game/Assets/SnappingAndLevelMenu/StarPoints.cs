using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarPoints : MonoBehaviour
{
    GameController gameController;

    [SerializeField] GameObject starEmpty1;
    [SerializeField] GameObject starFull1;

    [SerializeField] GameObject starEmpty2;
    [SerializeField] GameObject starFull2;

    [SerializeField] GameObject starEmpty3;
    [SerializeField] GameObject starFull3;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

   

}
