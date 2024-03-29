using UnityEngine;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{
    private Maze maze;
    private int targetIndex;
    private Vector3 targetPoistion;
    public void RePosition(Maze maze, int2 coordinates)
    {
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
        SceneManager.LoadScene(0);
    }
}
