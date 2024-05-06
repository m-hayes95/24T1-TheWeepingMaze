using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTRandomWalkTest : BTNode
{
    // Testing BT logic is working
    protected Vector3 nextDestination { get; set; }
    public float speed = 2; 
    public BTRandomWalkTest(BehaviorTree tree) : base(tree)
    {
        nextDestination = Vector3.zero;
        FindNextDestination();
    }

    public bool FindNextDestination()
    {
        object obj;
        bool found = false;
        found = Tree.Blackboard.TryGetValue("WorldBounds", out  obj);
        if (found)
        {
            Rect bounds = (Rect)obj;
            float x = Random.value * bounds.width;
            float y = Random.value * bounds.height;
            nextDestination = new Vector3(x, y, 0);
        }
        return found;
    }

    public override Result Execute()
    {
        if (Tree.gameObject.transform.position == nextDestination)
        {
               if (!FindNextDestination())
                return Result.FAILURE;
               else
                return Result.SUCCESS;
        }
        else
        {
            Tree.gameObject.transform.position = 
                Vector3.MoveTowards(Tree.gameObject.transform.position, 
                nextDestination, Time.deltaTime * speed);

            return Result.RUNNING;
        }
    }
}
