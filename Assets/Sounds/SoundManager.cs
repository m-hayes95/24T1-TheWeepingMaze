using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static  SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource toggleTorch, footStep1, footStep2, playerHit;
    [SerializeField] private AudioSource chasePlayerTrack, enemyAttack;
    [SerializeField] private AudioSource mouseClick;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;   
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlayChosenSound(AudioSource sound)
    {
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    private void PlayEachFootStep()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            if (!footStep1.isPlaying && !footStep2.isPlaying)
            {
                footStep1.Play();
            }
        }
        else
        {
            if (!footStep1.isPlaying && !footStep2.isPlaying)
            {
                footStep2.Play();
            }
        }
    }

    public void PlayPlayerFootStepSound() => PlayEachFootStep();
    public void PlayToggleTorchSound() => PlayChosenSound(toggleTorch);
    public void PlayPlayerHitSound() => PlayChosenSound(playerHit);
    public void PlayChasePlayerTrack() => PlayChosenSound(chasePlayerTrack);
    public void PlayEnemyAttackSound() => PlayChosenSound(enemyAttack);
    public void PlayMouseClickSound() => PlayChosenSound(mouseClick);
}
