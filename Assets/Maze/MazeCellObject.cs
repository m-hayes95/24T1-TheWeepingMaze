using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Created using tutorial: https://catlikecoding.com/unity/tutorials/prototypes/maze-2/

// Object pooling for the Maze cells - to improve performance

public class MazeCellObject : MonoBehaviour
{
#if UNITY_EDITOR 
    // Only run code if its being compiled in the Unity Editor
    // Do not include in final build of the game

    private static List<Stack<MazeCellObject>> pools;

    // Before loading the scene, check if there are any pools
    // If yes, clear them and make a new pool
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ClearPools()
    {
        if (pools == null) 
            pools = new();

        else
        {
            for (int i = 0; i < pools.Count; i++)
            {
                pools[i].Clear();
            }
        }
    }
#endif // Ends the conditional directive block started by #if UNITY_EDITOR

    [System.NonSerialized] // Don't save the fields value, start fresh each time
    System.Collections.Generic.Stack<MazeCellObject> pool;

    // Try to get instance of the MazeCellObject, if it avaialble return, else create a new instance
    public MazeCellObject GetInstance()
    {
        if (pool == null)
        {
            pool = new();
#if UNITY_EDITOR
            pools.Add(pool);
#endif
        }
        if (pool.TryPop(out MazeCellObject instance))
            instance.gameObject.SetActive(true);
        else
        {
            instance = Instantiate(this);
            instance.pool = pool;
        }
        return instance;
    }

    // Add the current istance to the top of the stack
    // - Back into the object pool for use later
    public void Recycle()
    {
        pool.Push(this); 
        gameObject.SetActive(false);
    }
}
