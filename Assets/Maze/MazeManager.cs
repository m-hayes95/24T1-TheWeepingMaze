using TMPro;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

public class MazeManager : MonoBehaviour
{
    public
    MazeVisualisation visualisation;
    [SerializeField] private int2 mazeSize = int2(20,20);
    Maze maze;

    private void Awake()
    {
        maze = new Maze(mazeSize);
        visualisation.Visualise(maze);
    }
}
