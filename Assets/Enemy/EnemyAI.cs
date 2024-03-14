using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Controls the enemy agents behaviour.
    [SerializeField, Range(0f, 5f), Tooltip("Set the speed in which the enemy will chase the player.")] 
    private float chaseSpeed = 1.5f;
    [SerializeField, Range(0f, 10f), Tooltip("Set the distance for when the enemy will start chasing the player, and lose pursuit.")] 
    private float distanceFromPlayerChaseThreshold = 5f;
    [SerializeField, Range(0f, 1f), Tooltip("Set the distance for when the enemy will attack the player " +
        "(If set below the Nav Mesh Agent Stopping Distance, the value will be automatically set to the same as the stopping distance value) .")]
    private float distanceFromPlayerAttackThreshold = 0.5f;
    [SerializeField, Range(0f, 10f), Tooltip("Set the time it takes for the enemy to return back to idel from its frozen state.")] 
    private float freezeTimer;
    [SerializeField, Range(0f, 10f), Tooltip("Set the enemy attack cooldown.")]
    private float attackResetTime = 3f;

    private enum EnemySM { Idle, Chase, Freeze, Attack, AttackCooldown };
    [SerializeField] private EnemySM enemySM; // Change TO DO
    private NavMeshAgent agent;
    private Player player;
    private Torch torch;
    private float distanceFromPlayer;
    private bool isTimerOn = false; // Do timer once
    private bool canAttack = true;
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
        // Ensure that the player can attack if the threshold is set too low
        if (distanceFromPlayerAttackThreshold < agent.stoppingDistance)
        {
            distanceFromPlayerAttackThreshold = agent.stoppingDistance;
        }
        //Debug.Log($" Distance: {distanceFromPlayer} Attack at: {distanceFromPlayerAttackThreshold}");

        switch (enemySM)
        {
            case EnemySM.Idle:
                agent.speed = 0f;
                if (distanceFromPlayer <= distanceFromPlayerChaseThreshold)
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
                else
                {
                    Debug.LogWarning($"{gameObject.name} is not on nav mesh. Failed Chase state. Current state = {enemySM}");
                    enemySM = EnemySM.Idle;
                }
                if (distanceFromPlayer >= distanceFromPlayerChaseThreshold)
                {
                    enemySM = EnemySM.Idle;
                } 
                if (distanceFromPlayer <= distanceFromPlayerAttackThreshold 
                    && canAttack)
                {
                    Debug.Log("Player in attack range");
                    enemySM = EnemySM.Attack;
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

            case EnemySM.Attack:
                agent.speed = 0f;
                if (agent.isOnNavMesh && canAttack)
                {
                    Attack();
                    enemySM = EnemySM.AttackCooldown;
                } else
                {
                    Debug.LogWarning($"{gameObject.name}  Failed to attack Player. Current state = {enemySM}");
                    enemySM = EnemySM.AttackCooldown;
                }
                break;

            case EnemySM.AttackCooldown:
                agent.speed = 0f;
                StartCoroutine(AttackResetTimer(attackResetTime));
                break;
            default:
                break;
        }
    }

    private void Chase()
    {
        //Debug.Log("Moving");
        agent.destination = player.transform.position;
    }

    private void Attack()
    {
        canAttack = false;
        int damageDealt = 1;
        player.TakeDamage(damageDealt);
        Debug.Log("player took damage");
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
    private IEnumerator AttackResetTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        canAttack = true;
        if (distanceFromPlayer >= distanceFromPlayerChaseThreshold)
        {
            enemySM = EnemySM.Idle;
        } 
        else
        {
            enemySM = EnemySM.Chase;
        }
        
    }




}

