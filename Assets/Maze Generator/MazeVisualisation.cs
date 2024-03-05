using Unity.Collections;
using UnityEngine;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/

[CreateAssetMenu]
public class MazeVisualisation : ScriptableObject
{
    [SerializeField] private MazeCellObject deadEndMaze;
    [SerializeField] private MazeCellObject straightMaze;
    [SerializeField] private MazeCellObject cornerMaze;
    [SerializeField] private MazeCellObject xMaze;
    [SerializeField] private MazeCellObject tMaze;

    // Spawn in instancs of maze pieces using locations from Maze struct
    public void Visualise(Maze maze)
    {
        for (int i = 0; i < maze.Length; i++)
        {
            MazeCellObject instance = xMaze.GetInstance();
            instance.transform.localPosition = maze.IndexToWorldPosition(i);
        }
    }
}




