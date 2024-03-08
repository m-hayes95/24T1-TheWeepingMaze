using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Maze maze;
    int targetIndex;
    Vector3 targetPoistion;

    public void Awake()
    {
        
    }
    public void SpawnEnemies(Maze maze, int2 coordinates)
    {
        this.maze = maze;
        targetIndex = maze.CoordinatesToIndex(coordinates);
        targetPoistion = transform.localPosition =
            maze.CoordinatesToWorldPosition(coordinates, transform.localPosition.y);
    }
}
