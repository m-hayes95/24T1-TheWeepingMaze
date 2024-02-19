using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private GameObject _torch;
    [SerializeField] [Range(0f,1000f)] private float rotateSpeed;
    private float mouseMovement = 0f;
    
    private bool isTorchOn;

    private void Start()
    {
        isTorchOn = true;
    }

    private void Update()
    {
        mouseMovement = Input.GetAxisRaw("Mouse X");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTorchOn) ActivateTorch();
            else DeActivateTorch();
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
        _torch.transform.RotateAround(transform.position, Vector3.forward,
            mouseMovement * Time.deltaTime * -rotateSpeed);
    }

    
}
