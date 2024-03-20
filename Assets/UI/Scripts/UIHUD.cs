using UnityEngine;
using UnityEngine.UI;

public class UIHUD : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Torch torch;
    [SerializeField] private GameObject playerHUD;

    private void OnEnable()
    {
        Torch.OnBatteryZero += TurnPlayerHUDOff;
    }

    private void OnDisable()
    {
        Torch.OnBatteryZero -= TurnPlayerHUDOff;    
    }

    private void Start()
    {
        healthBarSlider.maxValue = torch.GetRemainingBatteryTime();
    }
    private void Update()
    {
        healthBarSlider.value = torch.GetRemainingBatteryTime();
    }

    private void TurnPlayerHUDOff()
    {
        playerHUD.SetActive(false);
    }

}
