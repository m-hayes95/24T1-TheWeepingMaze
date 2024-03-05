using TMPro;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

public class MazeManager : MonoBehaviour
{
    [SerializeField] private MazeVisualisation visualisation;
    [SerializeField] private int2 mazeSize = int2(20,20);
    private Maze maze;

    private void Awake()
    {
        maze = new Maze(mazeSize);
        visualisation.Visualise(maze);
    }
}
