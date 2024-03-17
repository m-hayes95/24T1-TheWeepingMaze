using System.Collections;
using UnityEngine;  

public class Torch : MonoBehaviour
{
    // Event for when the torches battery reaches 0
    public delegate void BatteryZeroAction();
    public static event BatteryZeroAction OnBatteryZero;

    [SerializeField, Tooltip("Add a reference to the torch game object here.")] 
    private GameObject _torch;
    [SerializeField, Range(0f, 60f), Tooltip("Set how long the battery will last for (set how long the game will last for).")] 
    private float maxBatteryHealth;
    [SerializeField, Range(2f, 5f), Tooltip("Set the decay rate for battery health when the torch is ON.")]
    private float torchOnDecayMultiplier;
    [SerializeField, Range(1f, 5f), Tooltip("Set the decay rate for battery health when the torch is OFF (A value of 1 is equal to -1 each second).")]
    private float torchOffDecayMultiplier;
    [SerializeField, Range(0f, 2f), Tooltip("Set how quickly the torches light's intensity will decrease.")]
    private float intensityDecrease = .5f;

    private new Light light;
    private float initalLightIntensity;
    private float batteryHealth;
    private bool isTorchOn;
    public bool IsTorchOn
    {
        get { return isTorchOn; }
        set { isTorchOn = value; }
    }

    private void Start()
    {
        isTorchOn = false;
        batteryHealth = maxBatteryHealth;
        light = _torch.GetComponentInChildren<Light>();
        initalLightIntensity = light.intensity; // Store the original value
    }

    private void Update()
    {
        if (GameManager.isGameRunning)
        {
            //Debug.Log($"Is torch on {isTorchOn} "); 
            // Reduce the battery health over time, reduce by less if its switched off 
            if (batteryHealth > 0)
            {
                ReduceLightBatteryAndIntensityOverTime();   
            }

            if (batteryHealth <= 0)
            {
                batteryHealth = 0;
                light.intensity = 0;
                OnBatteryZero();
            }
        }
        
    }

    public void ToggleTorch()
    {
        _torch.SetActive(!_torch.activeSelf);
        isTorchOn = !isTorchOn;
    }
    public void TakeDamage(float damage)
    {
        batteryHealth -= damage;
    }
    public void ResetTorch()
    {
        batteryHealth = maxBatteryHealth;
        light.intensity = initalLightIntensity;
    }
    public float GetRemainingBatteryTime()
    {
        return batteryHealth;
    }

    private void ReduceLightBatteryAndIntensityOverTime()
    {
        if (IsTorchOn)
        {
            batteryHealth -= Time.deltaTime * torchOnDecayMultiplier;
        }
        else
        {
            batteryHealth -= Time.deltaTime / torchOffDecayMultiplier;
        }

        light.intensity -= intensityDecrease * Time.deltaTime * (light.intensity / batteryHealth);
    }
}
