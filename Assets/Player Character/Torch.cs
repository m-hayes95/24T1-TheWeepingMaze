using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private GameObject _torch;
    [SerializeField][Range(0f, 1000f)] private float rotateSpeed;
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
        isTorchOn = true;
        batteryCurrentTime = batteryMaxTime;
    }

    private void Update()
    {
        Debug.Log($"Is torch on {IsTorchOn} "); 

        mouseMovement = Input.GetAxisRaw("Mouse X");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTorchOn && !isTorchCharging) ActivateTorch();
            else DeActivateTorch();
        }

        if (batteryCurrentTime >= 0f && isTorchOn)
        {
            batteryCurrentTime -= Time.deltaTime;
            if (batteryCurrentTime <= 0f)
            {
                StartCoroutine(ChargeBattery());
            }
        }
        
        
    }

    private void FixedUpdate()
    {
        FollowMouse();
    }

    private void ActivateTorch()
    {
        isTorchOn = true;
        _torch.SetActive(true);
    }

    private void DeActivateTorch()
    {
        isTorchOn = false;
        _torch.SetActive(false);
    }

    private void FollowMouse()
    {
        _torch.transform.RotateAround(transform.position, Vector3.up,
            mouseMovement * Time.deltaTime * rotateSpeed);
    }

    private IEnumerator ChargeBattery()
    {
        DeActivateTorch();
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
