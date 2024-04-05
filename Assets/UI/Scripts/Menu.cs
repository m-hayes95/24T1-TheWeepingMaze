using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private bool isGamePaused = false;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject settingsMenu;
    private MazeManager mazeManager;
    private void Start()
    {
        mazeManager = FindObjectOfType<MazeManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused) PauseGame();
            else ResumeGame();
        }
    }
    private void PauseGame()
    {
        isGamePaused = true;
        menu.SetActive(true);
        menuButtons.SetActive(true);    
        playerHUD.SetActive(false);
        GameManager.isGameRunning = false;
        // Close settings tab if it was left open
        if (settingsMenu.activeInHierarchy)
        {
            settingsMenu.SetActive(false);
        }
        SoundManager.Instance.PlayMouseClickSound();
        if (GameManager.showDebugForIsGameRunningStatus)
            Debug.Log($"is game running set to {GameManager.isGameRunning}");
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        menu.SetActive(false);
        playerHUD.SetActive(true); 
        GameManager.isGameRunning = true;
        SoundManager.Instance.PlayMouseClickSound();
        if (GameManager.showDebugForIsGameRunningStatus)
            Debug.Log($"is game running set to {GameManager.isGameRunning}");
    }
    public void RestartGame()
    {
        SoundManager.Instance.PlayMouseClickSound();
        mazeManager.ReBuildMaze();
    }

    public void ReloadScene()
    {
        SoundManager.Instance.PlayMouseClickSound();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        SoundManager.Instance.PlayMouseClickSound();
        Application.Quit(); 
    }
}
