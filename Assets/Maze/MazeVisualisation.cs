using UnityEngine;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/

[CreateAssetMenu(menuName = "Maze Data")] 
public class MazeVisualisation : ScriptableObject
{
    [SerializeField] private MazeCellObject
        deadEndMaze, straightMaze,
        cornerMaze,
        tMaze,
        xMaze;

    static private Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0, 90f, 0),
        Quaternion.Euler(0, 180f, 0),
        Quaternion.Euler(0, 270f, 0),
    };


    // Spawn in instancs of maze pieces using locations from Maze struct
    public void Visualise(Maze maze)
    {
        
        for (int i = 0; i < maze.Length; i++)
        {
            // using tuple struct
            (MazeCellObject, int) prefabWithRotation = GetPrefab(maze[i]); 
            MazeCellObject instance = prefabWithRotation.Item1.GetInstance();
            instance.transform.SetPositionAndRotation(
                maze.IndexToWorldPosition(i), rotations[prefabWithRotation.Item2]
                );
        }
    }
    
    (MazeCellObject, int) GetPrefab(MazeFlag flags) => flags switch
    {
        MazeFlag.PassageN => (deadEndMaze, 0),
        MazeFlag.PassageE => (deadEndMaze, 1),
        MazeFlag.PassageS => (deadEndMaze, 2),
        MazeFlag.PassageW => (deadEndMaze, 3),

        MazeFlag.PassageN | MazeFlag.PassageS => (straightMaze, 0),
        MazeFlag.PassageE | MazeFlag.PassageW => (straightMaze, 1),

        MazeFlag.PassageN | MazeFlag.PassageE => (cornerMaze, 0),
        MazeFlag.PassageE | MazeFlag.PassageS => (cornerMaze, 1),
        MazeFlag.PassageS | MazeFlag.PassageW => (cornerMaze, 2),
        MazeFlag.PassageW | MazeFlag.PassageN => (cornerMaze, 3),

        MazeFlag.PassageAll & ~MazeFlag.PassageN => (tMaze, 0),
        MazeFlag.PassageAll & ~MazeFlag.PassageE => (tMaze, 1),
        MazeFlag.PassageAll & ~MazeFlag.PassageS => (tMaze, 2),
        MazeFlag.PassageAll & ~MazeFlag.PassageW => (tMaze, 3),

        _ => (xMaze, 0)

    };
}




