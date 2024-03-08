using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{
    private Maze maze;
    private int targetIndex;
    private Vector3 targetPoistion;
    public void FindPositionAndSpawn(Maze maze, int2 coordinates)
    {
        Instantiate(gameObject);
        gameObject.SetActive(false);
        this.maze = maze;
        targetIndex = maze.CoordinatesToIndex(coordinates);
        targetPoistion = transform.localPosition =
            maze.CoordinatesToWorldPosition(coordinates, transform.localPosition.y);
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            GameWin();
        }
    }

    private void GameWin()
    {
        Debug.Log($"Player wins the game with {FindObjectOfType<GameManager>().GetCurrentGameTime()}(s) left!");
        SceneManager.LoadScene(0);
    }
}
