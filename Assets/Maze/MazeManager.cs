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
    
    private Maze maze;

    
    private void Awake()
    {
        maze = new Maze(mazeSize);
        new GenerateMazeJob
        {
            maze = maze,
            seed = seed != 0 ? seed : Random.Range(1, int.MaxValue)
        }.Schedule().Complete();

        visualisation.Visualise(maze);
    }
}
