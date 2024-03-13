using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Controls the enemy agents behaviour.
    [SerializeField, Range(0f, 5f), Tooltip("Set the speed in which the enemy will chase the player.")] 
    private float chaseSpeed = 1.5f;
    [SerializeField, Range(0f, 10f), Tooltip("Set the distance for when the enemy will start chasing the player, and lose pursuit.")] 
    private float distanceFromPlayerThreshold = 5f;
    [SerializeField, Range(0f, 10f), Tooltip("Set the time it takes for the enemy to return back to idel from its frozen state.")] 
    private float freezeTimer;

    private enum EnemySM { Idle, Chase, Freeze };
    private EnemySM enemySM;
    private NavMeshAgent agent;
    private Player player;
    private Torch torch;
    private float distanceFromPlayer;
    private bool isTimerOn = false; // Do timer once
    private bool isEnemyInLight = false; 
    public bool IsEnemyInLight
    {
        get { return isEnemyInLight; }
        set { isEnemyInLight = value; }
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        torch = FindObjectOfType<Torch>();
        enemySM = EnemySM.Idle;
    }
    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        // Fix for when the torch turns off and the enemy did not exit the trigger, this bool stays on
        if (!torch.IsTorchOn)
        {
            isEnemyInLight = false;
        }
        switch (enemySM)
        {
            case EnemySM.Idle:
                agent.speed = 0f;
                if (distanceFromPlayer <= distanceFromPlayerThreshold)
                {
                    enemySM = EnemySM.Chase;
                }
                break;

            case EnemySM.Chase:
                agent.speed = chaseSpeed;
                if (agent.isOnNavMesh)
                {
                    Chase();
                }
                if (distanceFromPlayer > distanceFromPlayerThreshold)
                {
                    enemySM = EnemySM.Idle;
                }
                break;

            case EnemySM.Freeze:
                agent.speed = 0f;
                if (torch.IsTorchOn)
                {
                    if (!isTimerOn)
                    {
                        StartCoroutine(FreezeEnemyTimer());
                    }
                }
                
                break;
        }
    }

    private void Chase()
    {
        //Debug.Log("Moving");
        agent.destination = player.transform.position;
    }

    public void FreezeEnemy()
    {
        enemySM = EnemySM.Freeze;
    }

    private IEnumerator FreezeEnemyTimer()
    {
        isTimerOn = true;   
        yield return new WaitForSeconds(freezeTimer);
        isTimerOn = false;

        if (!isEnemyInLight)
        {
            enemySM = EnemySM.Idle;
        }
        else
        {
            StartCoroutine(FreezeEnemyTimer());
        }
    }

    
}

