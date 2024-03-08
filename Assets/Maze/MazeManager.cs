using System;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;
using Random = UnityEngine.Random;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/

public class MazeManager : MonoBehaviour
{
    [SerializeField, Tooltip("Attach reference to the Maze Visualisation Sciptable Object here.")] 
    private MazeVisualisation visualisation;
    [SerializeField, Tooltip("Set maze size (X and Z axis)")] 
    private int2 mazeSize = int2(20,20);
    [SerializeField, Tooltip("Use zero for a random seed.")] 
    private int seed;
    [SerializeField, Range(0f, 1f), 
        Tooltip("Chances of picking last instead of random (0 = low prob, 1 = higher prob)")] 
    private float pickLastProbability = 0.5f;
    [SerializeField, Range(0f, 1f), 
        Tooltip("How many dead ends should be opened (0 = none, 1 = higher possible amount)")]
    private float openDeadEndsProbability = 0.5f;
    [SerializeField, Range(0f, 1f),
        Tooltip("How many randomly choosen passages should be opened (0 = none, 1 = higher possible amount)")]
    private float openRandomPassageProbability = 0.5f;
    [SerializeField, Tooltip("Add reference to the player here")] 
    private Player player;
    [SerializeField, Range(0, 20), Tooltip("Set how many enemies will spawn in when the maze is generated.")]
    private int numberOfEnemies;
    [SerializeField, Tooltip("Place a game object here that has a EnemySpawner script component.")] 
    private GameObject enemy;
    [SerializeField, Tooltip("Place the end goal prefab here.")] 
    private EndGoal goal;
    [SerializeField, Tooltip("Higher the amount, the further the end goal will potentially spawn from the North-East corner of the Maze (Using X and Z axis). " +
        " Selecting 0 will spawn the goal in the furthest corner from the player.")]
    private int2 minGoalSpawnArea;

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

        // Spawn player - /4 to make sure player spawns in bottom left area,
        // by reducing the potential amount of size
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
            // Make sure enemies spawn away from the player,
            // by adding half the maze size to there spawn pos, if it is under half size
            var coordinates = int2(Random.Range(0, mazeSize.x), Random.Range(0, mazeSize.y));
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

        // Spawn End Goal
        minGoalSpawnArea.x = Mathf.Clamp(minGoalSpawnArea.x, 0, mazeSize.x);
        minGoalSpawnArea.y = Mathf.Clamp(minGoalSpawnArea.y, 0, mazeSize.y);
        // Need to -1 from max size or it will spawn outside the maze
        var maxSize = int2(mazeSize.x - 1, mazeSize.y - 1);
        var coordinatesGoal = int2(
            Random.Range(maxSize.x - minGoalSpawnArea.x, maxSize.x), 
            Random.Range(maxSize.y - minGoalSpawnArea.y, maxSize.y)
        );
        Debug.Log($" Coodinates for goal: {coordinatesGoal}");
        goal.FindPositionAndSpawn(maze, coordinatesGoal);
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
            Destroy(enemies[e].gameObject);
        }
        Destroy(goal.gameObject);
    }
}
