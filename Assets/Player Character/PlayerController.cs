using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Torch torch;
    private PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
        playerInput.Player.Torch.performed += GetTorchInput;
    }

    private void Start()
    {
        torch = GetComponent<Torch>();
    }

    public Vector2 GetMoveVectorNormalized()
    {
        return playerInput.Player.Move.ReadValue<Vector2>();
    }

    public void GetTorchInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            torch.ToggleTorch();
        }
    }
   
    private void OnDisable()
    {
        playerInput.Player.Torch.performed -= GetTorchInput;
        playerInput.Player.Disable();
    }


}
