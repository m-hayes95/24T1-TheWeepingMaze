using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

using static Unity.Mathematics.math;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/
public struct Maze
{
    private int2 size;
    // Disable access restrictions for parallel jobs so they can check neighbours
    [NativeDisableParallelForRestriction] NativeArray<MazeFlag> cells;
    
    // Index property that goes to native array
    public MazeFlag this[int index]
    {
        get => cells[index];   
        set => cells[index] = value;
    }
    public int Length => cells.Length;

    public int SizeEW => size.x;
    public int SizeNS => size.y;

    // Getters for index offset steps in each direction
    public int StepN => size.x;
    public int StepE => 1;
    public int StepS => -size.x;
    public int StepW => -1;
    public Maze(int2 size)
    {
        this.size = size;
        cells = new NativeArray<MazeFlag>(size.x * size.y, Allocator.Persistent);
    }

    public void Dispose()
    {
        if (cells.IsCreated)
        {
            cells.Dispose();
        }
    }

    public MazeFlag Set(int index, MazeFlag mask) =>
        cells[index] = cells[index].With(mask);
    public MazeFlag Unset(int index, MazeFlag mask) =>
        cells[index] = cells[index].Without(mask);
    public int2 IndexToCoordinates(int index)
    {
        int2 coordinates;
        coordinates.y = index / size.x;
        coordinates.x = index - size.x * coordinates.y;
        return coordinates;
    }

    public Vector3 CoordinatesToWorldPosition(int2 coordinates, float y = 0f) =>
        new Vector3(
            2f * coordinates.x + 1f - size.x, 
            y,
            2f * coordinates.y + 1f - size.y
        );

    public Vector3 IndexToWorldPosition(int index, float y = 0f) =>
        CoordinatesToWorldPosition(IndexToCoordinates(index), y);

    public int CoordinatesToIndex(int2 coordinates) =>
        coordinates.y * size.x + coordinates.x;

}
