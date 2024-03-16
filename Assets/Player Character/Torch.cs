using System.Collections;
using UnityEngine;  

public class Torch : MonoBehaviour
{
    // Event for when the torches battery reaches 0
    public delegate void BatteryZeroAction();
    public static event BatteryZeroAction OnBatteryZero;

    [SerializeField, Tooltip("Add a reference to the torch game object here.")] 
    private GameObject _torch;
    [SerializeField][Range(0f, 60f), Tooltip("Set how long the battery will last for (set how long the game will last for).")] 
    private float maxBatteryHealth;

    private float batteryHealth;
    private bool doEventOnce = false; 
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
    }

    private void Update()
    {
        //Debug.Log($"Is torch on {isTorchOn} "); 
        // Reduce the battery health over time
        if (batteryHealth > 0)
        {
            batteryHealth -= Time.deltaTime;
        }

        if (batteryHealth <= 0 && !doEventOnce)
        {
            // Game Over
            OnBatteryZero();
            doEventOnce = true;
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

    public float GetRemainingBatteryTime()
    {
        return batteryHealth;
    }


}
