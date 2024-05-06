using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequencer : BTComposite
{
    // Sequencer similar to the AND gate (everything must return true)
    
    private int currentNode = 0; // Keep track of the current node
    public BTSequencer(BehaviorTree tree, BTNode[] children) : base(tree, children)
    {
    }

    public override Result Execute()
    {
        if (currentNode < Children.Count) 
        {
            Result result = Children[currentNode].Execute();
            // keep checking until the current child has finished running
            if (result == Result.RUNNING)
                return Result.RUNNING; 
            // if the child fails then return failure & reset current node
            else if (result == Result.FAILURE)
            {
                currentNode = 0;
                return Result.FAILURE;
            }
            // if the child returns success
            else
            {
                currentNode++;
                // check if all children have finished running, if not keep checking
                if (currentNode < Children.Count)
                    return Result.RUNNING;
                // if all children have finished running and all returned success,
                // finish overide execute
                else
                {
                    currentNode = 0;
                    return Result.SUCCESS;
                }   
            }
        }
        return Result.SUCCESS;
    }
}
