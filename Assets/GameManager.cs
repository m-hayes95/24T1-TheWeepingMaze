using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1000)] private float gameTimer;
    private void Update()
    {
        if (gameTimer > 0) gameTimer -= Time.deltaTime;

        if (gameTimer < 0) GameOver();
    } 

    private void GameOver()
    {
        Debug.Log("Game is over");
    }

    public float GetCurrentGameTime()
    {
        return gameTimer;
    }
}
