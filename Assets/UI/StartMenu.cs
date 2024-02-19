using UnityEngine;

public class StartMenu : MonoBehaviour
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
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        menu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit(); 
    }
}
