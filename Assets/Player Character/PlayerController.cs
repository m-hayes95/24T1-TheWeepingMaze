using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float speed;
    [SerializeField][Range(0, 5)] private int maxHp;
    [SerializeField] private int hp;

    private void Start()
    {
        hp = maxHp;
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            //Debug.Log($"Player hit {collision.collider.gameObject.name}");

            if (collision.collider.GetComponent<EnemyAI>())
            {
                hp --;
            }
        }
    }

    public int GetPlayerHp()
    {
        return hp;
    }
}
