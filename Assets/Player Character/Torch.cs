using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private GameObject _torch;
    [SerializeField][Range(0f, 60f)] private float batteryMaxTime;
    [SerializeField][Range(0f, 10f)] private float rechargeTimer;
    [SerializeField] private float batteryCurrentTime;
    private float mouseMovement = 0f;

    private bool isTorchOn;
    public bool IsTorchOn
    {
        get { return isTorchOn; }
        set { isTorchOn = value; }
    }
    private bool isTorchCharging = false;

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
                StartCoroutine(ChargeBattery());
            }
        }
    }
    public void ToggleTorch()
    {
        _torch.SetActive(!_torch.activeSelf);
        isTorchOn = !isTorchOn;
    }

    private IEnumerator ChargeBattery()
    {
        ToggleTorch();
        isTorchCharging = true;
        Debug.Log("Battery is Charging...");

        yield return new WaitForSeconds(rechargeTimer);
        batteryCurrentTime = batteryMaxTime;
        isTorchCharging = false;
        Debug.Log("Battery fully charged");
    }

    public float GetCurrentTorchTime()
    {
        return batteryCurrentTime;
    }
}
