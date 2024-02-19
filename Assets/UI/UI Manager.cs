using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TMP_Text timer;

    private void Update()
    {
        hp.text = FindObjectOfType<PlayerController>().GetPlayerHp().ToString();
        timer.text = FindObjectOfType<Torch>().GetCurrentTorchTime().ToString("0.0");
    }
}
