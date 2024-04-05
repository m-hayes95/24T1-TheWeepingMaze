using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameMenu : MonoBehaviour
{
    private AudioSource mouseClickSound;
    private void Start()
    {
        mouseClickSound = GetComponent<AudioSource>();  
    }
    public void StartGame()
    {
        PlayMouseClickSound();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        PlayMouseClickSound();
        Application.Quit(); 
    }
    private void PlayMouseClickSound()
    {
        if (!mouseClickSound.isPlaying) mouseClickSound.Play();
    }
}
