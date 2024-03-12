using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    // Controls the enemy agents behaviour.

    public float speed;
    private float frozenSpeed = 0f;
    [SerializeField] private float initialSpeed;
    public Transform pointA, pointB;
    [SerializeField] private enum EnemySM { PointA, PointB, Freeze };
    [SerializeField] private EnemySM enemySM;
    private Vector3 direction;
    public bool isEnemyInLight = false;

    private void Start()
    {
        initialSpeed = speed;
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        switch (enemySM)
        {
            case EnemySM.PointA:
                direction = (pointB.position - transform.position);
                if (Vector3.Distance(pointB.position, transform.position) < 0.1f)
                { 
                    StartCoroutine(TimerAtPointB());
                }
                break;

            case EnemySM.PointB:
                direction = (pointA.position - transform.position);
                if (Vector3.Distance(pointA.position, transform.position) < 0.1f)
                {
                    StartCoroutine(TimerAtPointA());
                }
                break;

            case EnemySM.Freeze:
                speed = frozenSpeed;
                FreezeEnemy();
                // Dont let enemy move if they are in the light.
                if (!isEnemyInLight)
                {
                    StartCoroutine(FreezeEnemyTimer());
                }
                
                break;
                
        } 
    }

    private IEnumerator TimerAtPointA()
    {
        yield return new WaitForSeconds(2);
        enemySM = EnemySM.PointA;
    }

    private IEnumerator TimerAtPointB()
    {
        yield return new WaitForSeconds(2);
        enemySM = EnemySM.PointB;
    }

    private IEnumerator FreezeEnemyTimer()
    {
        yield return new WaitForSeconds(3);
        enemySM = EnemySM.PointB;
        speed = initialSpeed;
    }

    public void FreezeEnemy()
    {
        enemySM = EnemySM.Freeze;
    }
}

