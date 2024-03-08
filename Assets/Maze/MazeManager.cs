using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;
using Random = UnityEngine.Random;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/

public class MazeManager : MonoBehaviour
{
    [SerializeField] private MazeVisualisation visualisation;
    [SerializeField] private int2 mazeSize = int2(20,20);
    [SerializeField, Tooltip("Use zero for a random seed.")] private int seed;
    [SerializeField, Range(0f, 1f), Tooltip("Chances of picking last instead of random (0 = low prob, 1 = higher prob)")] 
    private float pickLastProbability = 0.5f;
    [SerializeField, Range(0f, 1f), 
        Tooltip("How many dead ends should be opened (0 = none, 1 = higher possible amount)")]
    private float openDeadEndsProbability = 0.5f;
    [SerializeField, Range(0f, 1f),
        Tooltip("How many randomly choosen passages should be opened (0 = none, 1 = higher possible amount)")]
    private float openRandomPassageProbability = 0.5f;
    [SerializeField] private Player player;
    [SerializeField, Range(0, 20), Tooltip("Set how many enemies will spawn in when the maze is generated.")]
    private int numberOfEnemies;
    [SerializeField, Tooltip("Place a game object here that has a EnemySpawner script component.")] 
    private GameObject enemy;

    private List<EnemySpawner> enemies;
    private MazeCellObject[] cellObjects;
    private Maze maze;


    private void Awake()
    {
        // Build maze
        maze = new Maze(mazeSize);

        new FindDiagonalPassagesJob 
        { 
            maze = maze  
        }.ScheduleParallel(
            maze.Length, maze.SizeEW, new GenerateMazeJob
            {
                maze = maze,
                seed = seed != 0 ? seed : Random.Range(1, int.MaxValue),
                pickLastProbability = pickLastProbability,
                openDeadEndProbability = openDeadEndsProbability,
                openRandomPassageProbability = openRandomPassageProbability
            }.Schedule()
        ).Complete();

        if (cellObjects == null || cellObjects.Length != maze.Length)
        {
            cellObjects = new MazeCellObject[maze.Length];
        }
        visualisation.Visualise(maze, cellObjects);

        // Spawn player - /4 to make sure player spawns in bottom left area
        if (seed != 0)
        {
            Random.InitState(seed);
        }
        player.FindStartPosition(maze.CoordinatesToWorldPosition(
            int2(Random.Range(0, mazeSize.x / 4), Random.Range(0, mazeSize.y / 4))
                )
            );

        // Spawn enemies at random locations throughout the maze
        enemies = new List<EnemySpawner>();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject newEnemy = Instantiate(enemy);
            if (enemy.GetComponent<EnemySpawner>())
            {
                newEnemy.SetActive(false);
                enemies.Add(newEnemy.GetComponent<EnemySpawner>());
            }
            else
            {
                Debug.LogError($"{enemy.gameObject.name} does not have an EnemySpawner attached!");
                Destroy(newEnemy);
            }
        }

        int2 halfsize = mazeSize / 2;
          for (int i = 0; i < enemies.Count; i++) 
        {
            var coordinates = int2(Random.Range(0, mazeSize.x), Random.Range(0, mazeSize.y));
            // Make sure enemies spawn away from the player
            if (coordinates.x < halfsize.x && coordinates.y < halfsize.y)
            {
                if (Random.value < 0.5f)
                {
                    coordinates.x += halfsize.x;
                }
                else
                {
                    coordinates.y += halfsize.y;
                }
            }
            enemies[i].SpawnEnemies(maze, coordinates);
            
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            EndGame();
        }
    }

    void EndGame()
    {
        for (int i = 0; i < cellObjects.Length; i++)
        {
            cellObjects[i].Recycle();
        }
        for (int e = 0; e < enemies.Count; e++)
        {
            enemies[e].gameObject.SetActive(false);
        }
    }
}
