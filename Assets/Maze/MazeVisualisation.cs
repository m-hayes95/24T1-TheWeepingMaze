using UnityEngine;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/

[CreateAssetMenu(menuName = "Maze Cell Data")]
public class MazeVisualisation : ScriptableObject
{
    [SerializeField]
    private MazeCellObject
        deadEndMaze, straightMaze,
        cornerMazeClosed, cornerMazeOpen,
        tMazeClosed, tMazeOpenNE, tMazeOpenSE, tMazeOpenNE_SE,
        xMazeClosedAll, xMazeClosedNE, xMazeOpenNE, xMazeOpenNE_SE, xMazeOpenNE_SW, xMazeOpenAll;

    static private Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0, 90f, 0),
        Quaternion.Euler(0, 180f, 0),
        Quaternion.Euler(0, 270f, 0),
    };


    // Spawn in instancs of maze pieces using locations from Maze struct
    public void Visualise(Maze maze, MazeCellObject[] cellObjects)
    {

        for (int i = 0; i < maze.Length; i++)
        {
            // using tuple struct
            (MazeCellObject, int) prefabWithRotation = GetPrefab(maze[i]);
            MazeCellObject instance = cellObjects[i] = prefabWithRotation.Item1.GetInstance();
            instance.transform.SetPositionAndRotation(
                maze.IndexToWorldPosition(i), rotations[prefabWithRotation.Item2]
                );
        }
    }

    (MazeCellObject, int) GetPrefab(MazeFlag flags) => flags.StraightPassages() switch
    {
        MazeFlag.PassageN => (deadEndMaze, 0),
        MazeFlag.PassageE => (deadEndMaze, 1),
        MazeFlag.PassageS => (deadEndMaze, 2),
        MazeFlag.PassageW => (deadEndMaze, 3),

        MazeFlag.PassageN | MazeFlag.PassageS => (straightMaze, 0),
        MazeFlag.PassageE | MazeFlag.PassageW => (straightMaze, 1),

        MazeFlag.PassageN | MazeFlag.PassageE => GetCorner(flags, 0),
        MazeFlag.PassageE | MazeFlag.PassageS => GetCorner(flags, 1),
        MazeFlag.PassageS | MazeFlag.PassageW => GetCorner(flags, 2),
        MazeFlag.PassageW | MazeFlag.PassageN => GetCorner(flags, 3),

        MazeFlag.PassageStraight & ~MazeFlag.PassageW => GetTMaze(flags, 0),
        MazeFlag.PassageStraight & ~MazeFlag.PassageN => GetTMaze(flags, 1),
        MazeFlag.PassageStraight & ~MazeFlag.PassageE => GetTMaze(flags, 2),
        MazeFlag.PassageStraight & ~MazeFlag.PassageS => GetTMaze(flags, 3),

        _ => GetXJunction(flags)

    };

    (MazeCellObject, int) GetCorner(MazeFlag flags, int rotation) => (
        flags.HasAny(MazeFlag.PassageDiagonal) ? cornerMazeOpen : cornerMazeClosed, rotation
        );

    (MazeCellObject, int) GetTMaze(MazeFlag flags, int rotation) => (
        flags.RotatedDiagonalPassages(rotation) switch
        {
            MazeFlag.Empty => tMazeClosed,
            MazeFlag.PassageNE => tMazeOpenNE,
            MazeFlag.PassageSE => tMazeOpenSE,
            _ => tMazeOpenNE_SE
        },
        rotation
    );

    (MazeCellObject, int) GetXJunction(MazeFlag flags) =>
        flags.DiagonalPassages() switch
        {
            MazeFlag.Empty => (xMazeClosedAll, 0),

            MazeFlag.PassageNE => (xMazeOpenNE, 0),
            MazeFlag.PassageSE => (xMazeOpenNE, 1),
            MazeFlag.PassageSW => (xMazeOpenNE, 2),
            MazeFlag.PassageNW => (xMazeOpenNE, 3),

            MazeFlag.PassageNE | MazeFlag.PassageSE => (xMazeOpenNE_SE, 0),
            MazeFlag.PassageSE | MazeFlag.PassageSW => (xMazeOpenNE_SE, 1),
            MazeFlag.PassageSW | MazeFlag.PassageNW => (xMazeOpenNE_SE, 2),
            MazeFlag.PassageNW | MazeFlag.PassageNE => (xMazeOpenNE_SE, 3),

            MazeFlag.PassageNE | MazeFlag.PassageSW => (xMazeOpenNE_SW, 0),
            MazeFlag.PassageSE | MazeFlag.PassageNW => (xMazeOpenNE_SW, 1),

            MazeFlag.PassageDiagonal & ~MazeFlag.PassageNE => (xMazeClosedNE, 0),
            MazeFlag.PassageDiagonal & ~MazeFlag.PassageSE => (xMazeClosedNE, 1),
            MazeFlag.PassageDiagonal & ~MazeFlag.PassageSW => (xMazeClosedNE, 2),
            MazeFlag.PassageDiagonal & ~MazeFlag.PassageNW => (xMazeClosedNE, 3),

            _ => (xMazeOpenAll, 0),
        };
}




