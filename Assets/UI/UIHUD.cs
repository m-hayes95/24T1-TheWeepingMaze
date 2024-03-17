using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI torchTimer;
    [SerializeField] Torch torch;

    private void Update()
    {
        if (torchTimer != null)
            torchTimer.text = torch.GetRemainingBatteryTime().ToString("00.0");
    }
}
