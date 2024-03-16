using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0f,20f)] float speed;
    [SerializeField, Range(0f,100f)] float rotateSpeed;
    [SerializeField, Range(0f,1f)] float playerVertOffset;
    
    private PlayerController controller;
    private Animation walkingAnim;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
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
        PlayerMovement();
        RotatePlayerMousePosDelta();
    }

    public void FindStartPosition(Vector3 position)
    {
        Vector3 offsetZ = new Vector3(0, playerVertOffset, 0);
        controller.enabled = false;
        transform.position = position + offsetZ;
        controller.enabled = true;
    }

    
    private void PlayerMovement()
    {
        Vector2 inputVector = controller.GetMoveVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDirection * speed * Time.deltaTime;
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
