using UnityEngine;

public class TorchCollision : MonoBehaviour
{
    // Stop the enemy from moving when they are in the torch light.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            collision.gameObject.GetComponent<EnemyAI>().FreezeEnemy();
            collision.gameObject.GetComponent<EnemyAI>().isEnemyInLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            collision.gameObject.GetComponent<EnemyAI>().isEnemyInLight = false;
        }
    }
}
