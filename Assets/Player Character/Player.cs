using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0f,20f), Tooltip("Set the speed of the player.")] 
    private float speed;
    [SerializeField, Range(0f,100f), Tooltip("Set the roate speed of the player.")]
    private float rotateSpeed;
    [SerializeField, Range(0f,1f), Tooltip("Set a verticle offset the player will spawn above the ground.")] 
    private float playerVertOffset;
    [SerializeField, Range(0f,3f), Tooltip("Set the value of the camera shake intensity when hit.")] 
    private float cameraShakeIntensity = 1f;
    [SerializeField, Range(0f, 5f), Tooltip("Set the multipler of the knockback force.")] 
    private int knockbackForceMultipler = 4;

    private PlayerController controller;
    private Animation walkingAnim;
    private Rigidbody rb;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
        walkingAnim = GetComponentInChildren<Animation>();
    }

    private void Update()
    {
        if (walkingAnim != null)
        {
            Animate();
        } 
        else
        {
            Debug.LogWarning("Player walking animation is NULL, add reference");
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.isGameRunning)
        {
            PlayerMovement();
            RotatePlayerMousePosDelta();
        }
    }

    public void FindStartPosition(Vector3 position)
    {
        Vector3 offsetZ = new Vector3(0, playerVertOffset, 0);
        controller.enabled = false;
        transform.position = position + offsetZ;
        controller.enabled = true;
        gameObject.SetActive(true);
    }

    public void PlayerHit(Vector3 enemyPosition)
    {
        // Add knockback opposite from enemy position
        Vector3 knockbackDirection = (transform.position - enemyPosition).normalized;
        ApplyKnockback(knockbackDirection);
        // Add hit sound
            
        // Add red material??

        // Screenshake
        float shakeTimer = 0.1f;
        CameraShake.Instance.PlayCameraShake(cameraShakeIntensity, shakeTimer);
    }

    private void ApplyKnockback(Vector3 direction)
    {
        rb.velocity = Vector3.zero; // Reset velocity to prevent inconsitent events
        rb.AddForce(direction * knockbackForceMultipler, ForceMode.Impulse);
    }
    private void PlayerMovement()
    {
        Vector2 inputVector = controller.GetMoveVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * speed * Time.deltaTime;
        RotatePlayerToFoward(moveDirection);
    }
    private void RotatePlayerToFoward(Vector3 moveDirection)
    {
        // Rotate the player to the current forward position
        float rotateForwardSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection,
            rotateForwardSpeed * Time.deltaTime);
    }
    private void Animate()
    {
        // Only animate when the player is moving, else return the animation back to its inital point
        if (controller.GetMoveVectorNormalized() != Vector2.zero)
        {
            walkingAnim.Play();
        }
        else
        {
            walkingAnim.Rewind();
        }
    }
    // This rotation moves left and right when moving the mouse across the screen
    private void RotatePlayerMousePosDelta()
    {
        Vector2 mousePositionDelta = controller.GetMousePositionVectorDelta();
        // Smooth out the position changes overtime 
        float smoothMouseX = Mathf.Lerp(0, mousePositionDelta.x, Time.deltaTime * rotateSpeed);
        float smoothMouseY = Mathf.Lerp(0, mousePositionDelta.y, Time.deltaTime * rotateSpeed);
        transform.Rotate(Vector3.up * smoothMouseX);
        transform.Rotate(Vector3.down * smoothMouseY);
    }

    

    /*
    private void RotateToMousePosition()
    {
        Vector2 mousePos = controller.GetMousePositionVector();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.y));

        Vector3 lookDir = mouseWorldPos - transform.position;
        // Ensure the player stays upright
        lookDir.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(lookDir);

        // Smoothly rotate the player towards the current mouse position
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
    */

}
