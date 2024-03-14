using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Torch torch;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.TorchOnOff.performed += GetTorchInput;
    }

    private void Start()
    {
        torch = GetComponent<Torch>();
    }

    public Vector2 GetMoveVectorNormalized()
    {
        return playerInput.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMousePositionVector()
    {
        return playerInput.Player.Rotate.ReadValue<Vector2>();
    }

    private void GetTorchInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            torch.ToggleTorch();
        }
    }
   
    private void OnDisable()
    {
        playerInput.Player.TorchOnOff.performed -= GetTorchInput;
        playerInput.Player.Disable();
    }
}
