using Unity.Burst;
using System.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;
using UnityEngine.UIElements;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/

[BurstCompile]
public struct GenerateMazeJob : IJob
{
    public Maze maze;
    public int seed;
    public float pickLastProbability;
    public float openDeadEndProbability;
    public float openRandomPassageProbability;

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
            bool pickLast = random.NextFloat() < pickLastProbability;
            int randomActiveIndex, index;
            if (pickLast)
            {
                randomActiveIndex = 0;
                index = activeIndices[lastActiveIndex];
            }
            else
            {
                randomActiveIndex = random.NextInt(firstActiveIndex, lastActiveIndex + 1);
                index = activeIndices[randomActiveIndex];
            }
            
            int availablePassageCount = FindAvailablePassages(index, scratchpad);
            // No more available cells, go back
            if (availablePassageCount <= 1)
            {
                if (pickLast)
                {
                    lastActiveIndex -= 1;
                }
                else
                {
                    activeIndices[randomActiveIndex] = activeIndices[firstActiveIndex++];
                }
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
        if (openDeadEndProbability > 0)
        {
            random = OpenDeadEnds(random, scratchpad);
        }
        if (openRandomPassageProbability > 0)
        {
            random = OpenRandomPassage(random);
        }
    }

    Random OpenRandomPassage (Random random)
    {
        for (int i = 0; i < maze.Length; i++)
        {
            int2 coordinates = maze.IndexToCoordinates(i);
            if (coordinates.x > 0 && random.NextFloat() < openRandomPassageProbability)
            {
                maze.Set(i, MazeFlag.PassageW);
                maze.Set(i + maze.StepW, MazeFlag.PassageE);
            }
            if (coordinates.y > 0 && random.NextFloat() < openRandomPassageProbability)
            {
                maze.Set(i, MazeFlag.PassageS);
                maze.Set(i + maze.StepS, MazeFlag.PassageN);
            }
            
        }
        return random;
    }
    // After maze is built, go through and randomly replace dead end maze pieces - depending on multiplier
    Random OpenDeadEnds(
        Random random, NativeArray<(int, MazeFlag, MazeFlag)> scratchpad
        )
    {
        for (int i = 0; i < maze.Length; i++)
        {
            MazeFlag cell = maze[i];
            if (cell.HasExactlyOne() && random.NextFloat() < openDeadEndProbability)
            {
                int availablePassageCount = FindClosedPassages(i, scratchpad, cell);
                (int, MazeFlag, MazeFlag) passage = 
                    scratchpad[random.NextInt(0, availablePassageCount)];
                maze[i] = cell.With(passage.Item2);
                maze.Set(i + passage.Item1, passage.Item3);
            }
        }
        return random;
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
    int FindClosedPassages (
        int index, NativeArray<(int, MazeFlag, MazeFlag)> scratchpad, MazeFlag exclude
        )
    {
        int2 coordinates = maze.IndexToCoordinates( index );
        int count = 0;
        if (exclude != MazeFlag.PassageE && coordinates.x +1 < maze.SizeEW)
        {
            scratchpad[count++] = (maze.StepE, MazeFlag.PassageE, MazeFlag.PassageW);
        }
        if (exclude != MazeFlag.PassageW && coordinates.x > 0)
        {
            scratchpad[count++] = (maze.StepW, MazeFlag.PassageW, MazeFlag.PassageE);
        }
        if (exclude != MazeFlag.PassageN && coordinates.y + 1 < maze.SizeNS)
        {
            scratchpad[count++] = (maze.StepN, MazeFlag.PassageN, MazeFlag.PassageS);
        }
        if (exclude != MazeFlag.PassageS && coordinates.y > 0)
        {
            scratchpad[count++] = (maze.StepS, MazeFlag.PassageS, MazeFlag.PassageN);
        }
        return count;
    }
}


