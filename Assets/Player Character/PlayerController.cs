using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float speed;

    private void FixedUpdate()
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
}
