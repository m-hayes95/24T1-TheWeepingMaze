using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI torchTimer;

    private void Update()
    {
        torchTimer.text = FindObjectOfType<Torch>().GetRemainingBatteryTime().ToString("00.0");
    }
}
