using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private bool isGamePaused = false;
    [SerializeField] private GameObject menu;
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
        GameManager.isGameRunning = false;
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        menu.SetActive(false);
        GameManager.isGameRunning = true;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
}
