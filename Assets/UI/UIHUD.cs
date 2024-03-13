using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI torchTimer;
    [SerializeField] TextMeshProUGUI gameTimer;

    private void Update()
    {
        hp.text = FindObjectOfType<Player>().GetPlayerHp().ToString();
        torchTimer.text = FindObjectOfType<Torch>().GetCurrentTorchTime().ToString("00.0");
        gameTimer.text = FindObjectOfType<GameManager>().GetCurrentGameTime().ToString("00.00");
    }
}
