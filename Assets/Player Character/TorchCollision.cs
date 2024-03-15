using UnityEngine;

public class TorchCollision : MonoBehaviour
{
    private Collider collider;
    private void Start()
    {
        collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            Debug.LogWarning("No collider found in torch");
        }
    }
    // Stop the enemy from moving when they are in the torch light.
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            collision.gameObject.GetComponent<EnemyAI>().FreezeEnemy();
            collision.gameObject.GetComponent<EnemyAI>().IsEnemyInLight = true;

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            collision.gameObject.GetComponent<EnemyAI>().IsEnemyInLight = false;
        }
    }
}
