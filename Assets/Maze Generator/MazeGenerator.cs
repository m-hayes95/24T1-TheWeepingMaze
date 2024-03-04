using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private GameObject mazeComponent;
    [SerializeField] private int mazeX, mazeY;
    private void Update()
    {
        for (int i = 0; i < mazeX; i++)
        {
            for (int j = 0; j < mazeY; j++)
            {
                Vector3 spawnPoint = new Vector3(i, j, 0);
                Instantiate(mazeComponent, spawnPoint, Quaternion.identity);
            }
        }
    }
}
