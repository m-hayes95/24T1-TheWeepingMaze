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
    private Maze maze;

    
    private void Awake()
    {
        maze = new Maze(mazeSize);
        new GenerateMazeJob
        {
            maze = maze,
            seed = seed != 0 ? seed : Random.Range(1, int.MaxValue),
            pickLastProbability = pickLastProbability,
            openDeadEndProbability = openDeadEndsProbability,
            openRandomPassageProbability = openRandomPassageProbability
        }.Schedule().Complete();

        visualisation.Visualise(maze);
    }
}
