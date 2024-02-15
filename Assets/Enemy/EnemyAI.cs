using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    public float speed;
    public Transform pointA, pointB;
    [SerializeField] private enum EnemySM { PointA, PointB };
    [SerializeField] private EnemySM enemySM;
    private Vector3 direction;

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
}
