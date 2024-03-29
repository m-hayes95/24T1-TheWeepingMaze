using UnityEngine;
// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/


// Check which passages of the maze are currently open - using bit flags
public enum MazeFlag
{
    Empty = 0,
    PassageN = 0b0001,
    PassageE = 0b0010,
    PassageS = 0b0100,
    PassageW = 0b1000,

    PassageStraight = 0b1111,
    // Diagonal Passages
    PassageNE = 0b0001_0000,
    PassageSE = 0b0010_0000,
    PassageSW = 0b0100_0000,
    PassageNW = 0b1000_0000,

    PassageDiagonal = 0b1111_0000
}

public static class MazeFlagsExtensions
{
    public static bool Has(this MazeFlag flags, MazeFlag mask) => 
        (flags & mask) == mask;

    public static bool HasAny(this MazeFlag flags, MazeFlag mask) => 
        (flags & mask) != 0;

    public static bool HasNot(this MazeFlag flags, MazeFlag mask) =>
        (flags & mask) != mask;
    // Using bits so need to - 1
    public static bool HasExactlyOne(this MazeFlag flags) =>
        flags !=0 && (flags &(flags - 1)) == 0;

    public static MazeFlag With(this MazeFlag flags, MazeFlag mask) =>
        flags | mask;

    public static MazeFlag Without(this MazeFlag flags, MazeFlag mask) =>
        flags & ~mask;

    public static MazeFlag StraightPassages(this MazeFlag flags) =>
        flags & MazeFlag.PassageStraight;

    public static MazeFlag DiagonalPassages(this MazeFlag flags) =>
        flags & MazeFlag.PassageDiagonal;

    // Get and rotate diagonal flag rightways by max 4
    public static MazeFlag RotatedDiagonalPassages(this MazeFlag flags, int rotation)
    {
        int bits = (int)(flags & MazeFlag.PassageDiagonal);
        bits = (bits >> rotation) | (bits << (4 - rotation));
        return (MazeFlag)bits & MazeFlag.PassageDiagonal;
    }
}


