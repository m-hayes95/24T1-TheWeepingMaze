using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static  SoundManager Instance { get; private set; }

    private AudioSource chasePlayerTrack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;   
            DontDestroyOnLoad(gameObject);  
        }
        else 
            Destroy(gameObject);
    }

    private void Start()
    {
        chasePlayerTrack = GetComponent<AudioSource>(); 
    }

    private void ActivateChasePlayerTrack()
    {
        chasePlayerTrack.Play();
    }

    public void PlayChasePlayerTrack()
    {
        if (!chasePlayerTrack.isPlaying)
        {
            ActivateChasePlayerTrack();
        }
    }
}
