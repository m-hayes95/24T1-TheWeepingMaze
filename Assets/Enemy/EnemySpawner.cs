using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    private Maze maze;
    private int targetIndex;
    private Vector3 targetPoistion;


    public void SpawnEnemies(Maze maze, int2 coordinates)
    {
        this.maze = maze;
        gameObject.SetActive(true);
        targetIndex = maze.CoordinatesToIndex(coordinates);
        targetPoistion = transform.localPosition =
            maze.CoordinatesToWorldPosition(coordinates, transform.localPosition.y);
    }
}
