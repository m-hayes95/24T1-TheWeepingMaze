using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Range(0f,20f)] float speed;
    [SerializeField, Range(0f,1f)] float playerVertOffset;
    [SerializeField][Range(0, 5)] private int maxHp;
    
    private PlayerController controller;
    private int hp;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
    private void Start()
    {
        hp = maxHp;
    }

    private void FixedUpdate()
    {
        PlayerMovement();
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
        //Debug.Log(moveDirection);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            //Debug.Log($"Player hit {collision.collider.gameObject.name}");

            if (collision.collider.GetComponent<EnemyAI>())
            {
                hp--;
            }
        }
    }

    public int GetPlayerHp()
    {
        return hp;
    }
}
