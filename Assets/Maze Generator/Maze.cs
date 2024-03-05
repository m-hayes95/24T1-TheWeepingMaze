using System.Collections;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/
public struct Maze
{
    private int2 size;
    public int Length => size.x * size.y;
    public Maze(int2 size)
    {
        this.size = size;
    }

    public int2 IndexToCoordinates(int index)
    {
        int2 coordinates;
        coordinates.y = index / size.x;
        coordinates.x = index - size.x * coordinates.y;
        return coordinates;
    }

    public Vector3 CoordinatesToWorldPosition(int2 coordinates, float y = 0f) =>
        new Vector3(
            2f * coordinates.x + 1f - size.x, y,
            2f * coordinates.y + 1f - size.y
        );

    public Vector3 IndexToWorldPosition(int index, float y = 0f) =>
        CoordinatesToWorldPosition(IndexToCoordinates(index), y);

}
