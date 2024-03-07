using Unity.Burst;
using System.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/

[BurstCompile]
public struct GenerateMazeJob : IJob
{
    public Maze maze;
    public int seed;

    // Using depth-first search alogrithm to run through the maze
    public void Execute()
    {
        var random = new Random((uint)seed);
        var scratchpad = new NativeArray<(int, MazeFlag, MazeFlag)>(
            4, Allocator.Temp, NativeArrayOptions.UninitializedMemory
            );
        var activeIndices = new NativeArray<int>(
            maze.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory
            );
        int firstActiveIndex = 0, lastActiveIndex = 0;
        activeIndices[firstActiveIndex] = random.NextInt(maze.Length);

        // Get the last active maze cell and find passages for it
        while (firstActiveIndex <= lastActiveIndex)
        {
            int index = activeIndices[lastActiveIndex];
            int availablePassageCount = FindAvailablePassages(index, scratchpad);
            // No more available cells, go back
            if (availablePassageCount <= 1)
            {
                lastActiveIndex -= 1;
            }
            // Find next available cell
            if (availablePassageCount > 0)
            {
                (int, MazeFlag, MazeFlag) passage = scratchpad[random.NextInt(
                    0, availablePassageCount
                    )];
                maze.Set(index, passage.Item2);
                maze[passage.Item1] = passage.Item3;
                activeIndices[++lastActiveIndex] = passage.Item1;
            }
        }
    }

    // Check if current maze cell has an empty neighbour
    int FindAvailablePassages(int index, NativeArray<(int, MazeFlag, MazeFlag)> scratchpad)
    {
        int2 coordinates = maze.IndexToCoordinates(index);
        int count = 0;
        if (coordinates.x +1 < maze.SizeEW)
        {
            int i = index + maze.StepE;
            if (maze[i] == MazeFlag.Empty)
            {
                scratchpad[count++] = (i, MazeFlag.PassageE, MazeFlag.PassageW);
            }
        }
        if (coordinates.x > 0)
        {
            int i = index + maze.StepW;
            if (maze[i] == MazeFlag.Empty)
            {
                scratchpad[count++] = (i, MazeFlag.PassageW, MazeFlag.PassageE);
            }
        }
        if (coordinates.y + 1 < maze.SizeNS)
        {
            int i = index + maze.StepN;
            if (maze[i] == MazeFlag.Empty)
            {
                scratchpad[count++] = (i, MazeFlag.PassageN, MazeFlag.PassageS);
            }
        }
        if (coordinates.y > 0)
        {
            int i = index + maze.StepS;
            if (maze[i] == MazeFlag.Empty)
            {
                scratchpad[count++] = (i, MazeFlag.PassageS, MazeFlag.PassageN);
            }
        }
        return count;
    }
}
