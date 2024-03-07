using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;


[BurstCompile]
public struct FindDiagonalPassagesJob : IJobFor
{
    public Maze maze;
    public void Execute(int i)
    {
        MazeFlag cell = maze[i];
        if (
            cell.Has(MazeFlag.PassageN | MazeFlag.PassageE) &&
            maze[i + maze.StepN + maze.StepE].Has(MazeFlag.PassageS | MazeFlag.PassageW)
        )
        {
            cell = cell.With(MazeFlag.PassageNE);
        }
        if (
            cell.Has(MazeFlag.PassageN | MazeFlag.PassageW) &&
            maze[i + maze.StepN + maze.StepW].Has(MazeFlag.PassageS | MazeFlag.PassageE)
        )
        {
            cell = cell.With(MazeFlag.PassageNW);
        }
        if (
            cell.Has(MazeFlag.PassageS | MazeFlag.PassageE) &&
            maze[i + maze.StepS + maze.StepE].Has(MazeFlag.PassageN | MazeFlag.PassageW)
        )
        {
            cell = cell.With(MazeFlag.PassageSE);
        }
        if (
            cell.Has(MazeFlag.PassageS | MazeFlag.PassageW) &&
            maze[i + maze.StepS + maze.StepW].Has(MazeFlag.PassageN | MazeFlag.PassageE)
        )
        {
            cell = cell.With(MazeFlag.PassageSW);
        }
        maze[i] = cell;
    }
}
