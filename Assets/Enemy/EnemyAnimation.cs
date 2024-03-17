using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // Lost Relic Games - Escaping Unity Animator Hell: https://youtu.be/nBkiSJ5z-hE?si=TTKlVAdXPBuuwVBz

    private Animator animator;
    private string currentState;
    private const string ENEMY_START_CHASE = "EnemyChasingStart";
    private const string ENEMY_END_CHASE = "EnemyChasingEnd";
    private const string ENEMY_ATTACK = "EnemyAttack";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) // Check if the animation is already playing
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }
    public void PlayStartChaseAnimation()
    {
        ChangeAnimationState(ENEMY_START_CHASE);
    }
    public void PlayEndChaseAnimation()
    {
        ChangeAnimationState(ENEMY_END_CHASE);
    }
    public void PlayAttackAnimation()
    {
        ChangeAnimationState(ENEMY_ATTACK);
    }
}
