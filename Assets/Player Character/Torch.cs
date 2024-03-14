using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField, Tooltip("Add a reference to the torch game object here.")] 
    private GameObject _torch;
    [SerializeField][Range(0f, 60f), Tooltip("Set how long the battery will last for.")] 
    private float batteryMaxTime;
    [SerializeField][Range(0f, 10f), Tooltip("Set how fast the battery will recharge at (Only used when the battery is fully empty).")] 
    private float fromEmptyRechargeTimer;
    
    [SerializeField, Tooltip("Set the rate of how fast / slow the torch will charge (Except for when charging from empty!). " +
        "Time in seconds is divided by the set number (E.g. 2 will divide each second in half).")] 
    private float rechargeRate;

    private float batteryCurrentTime;
    private bool isTorchCharging = false;
    private bool isTorchOn;
    public bool IsTorchOn
    {
        get { return isTorchOn; }
        set { isTorchOn = value; }
    }
    

    private void Start()
    {
        isTorchOn = false;
        batteryCurrentTime = batteryMaxTime;
    }

    private void Update()
    {
        //Debug.Log($"Is torch on {isTorchOn} "); 

        if (batteryCurrentTime >= 0f && isTorchOn)
        {
            batteryCurrentTime -= Time.deltaTime;
            if (batteryCurrentTime <= 0f)
            {
                batteryCurrentTime = 0f;
                StartCoroutine(ChargeBatteryFromEmpty());
            }
        }

        ChargeBatteryOvertime();
    }
    public void ToggleTorch()
    {
        if (!isTorchCharging)
        {
            _torch.SetActive(!_torch.activeSelf);
            isTorchOn = !isTorchOn;
        }
    }

    // When the battery dies whilst the torch is on,
    // the player has to wait until its completely recharged to toggle back on.
    private IEnumerator ChargeBatteryFromEmpty()
    {
        ToggleTorch();
        isTorchCharging = true;
        //Debug.Log("Battery is Charging...");

        yield return new WaitForSeconds(fromEmptyRechargeTimer);
        isTorchCharging = false;
        batteryCurrentTime = batteryMaxTime;
        //Debug.Log("Battery fully charged");
    }

    // The battery will charge over time when not in use
    private void ChargeBatteryOvertime()
    {
        if (!isTorchOn && !isTorchCharging &&
            batteryCurrentTime < batteryMaxTime)
        {
            batteryCurrentTime += (Time.deltaTime / rechargeRate);
            if (batteryCurrentTime > batteryMaxTime) 
            {
                batteryCurrentTime = batteryMaxTime;
            }
        }
    }

    public float GetCurrentTorchTime()
    {
        return batteryCurrentTime;
    }
}
