using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();
    }

    public Vector2 GetMoveVectorNormalized()
    {
        return playerInput.Player.Move.ReadValue<Vector2>();
    }
   
    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
}
