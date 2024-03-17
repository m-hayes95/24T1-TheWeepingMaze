using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameRunning;

    [SerializeField] private GameObject endScreen;

    private void OnEnable()
    {
        Torch.OnBatteryZero += GameOver;
    }
    private void OnDisable()
    {
        Torch.OnBatteryZero -= GameOver;
    }

    private void Update()
    {
        Debug.Log($"Gameplay is running: {isGameRunning}");
    }
    private void GameOver()
    {
        Debug.Log("Game is over");
        endScreen.SetActive(true);
        isGameRunning = false;
    }

    
}
