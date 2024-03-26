using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private bool isGamePaused = false;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject playerHUD;
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
        playerHUD.SetActive(false);
        GameManager.isGameRunning = false;
        if (GameManager.showDebugForIsGameRunningStatus)
            Debug.Log($"is game running set to {GameManager.isGameRunning}");
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        menu.SetActive(false);
        playerHUD.SetActive(true); 
        GameManager.isGameRunning = true;
        if (GameManager.showDebugForIsGameRunningStatus)
            Debug.Log($"is game running set to {GameManager.isGameRunning}");
    }
    public void RestartGame()
    {
        mazeManager.ReBuildMaze();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
}
