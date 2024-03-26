using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameRunning = false;
    public static bool showDebugForIsGameRunningStatus = false; // So I can turn off all debugs that are letting me know they changed this variable

    [SerializeField] private GameObject endScreen;
    private Player player;

    private void OnEnable()
    {
        Torch.OnBatteryZero += GameOver;
    }
    private void OnDisable()
    {
        Torch.OnBatteryZero -= GameOver;
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        Debug.Log($"Gameplay is running: {isGameRunning}");
    }
    private void GameOver()
    {
        endScreen.SetActive(true);
        player.gameObject.SetActive(false);
        isGameRunning = false;
        if (showDebugForIsGameRunningStatus)
            Debug.Log($"is game running set to {isGameRunning}");
    }

    
}
