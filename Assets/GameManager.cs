using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        Torch.OnBatteryZero += GameOver;
    }
    private void OnDisable()
    {
        Torch.OnBatteryZero -= GameOver;
    }
    private void GameOver()
    {
        Debug.Log("Game is over");
    }

    
}
