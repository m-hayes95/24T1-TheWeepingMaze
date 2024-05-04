using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode 
{
    // Current status of the node
    public enum Result { RUNNING, FAILURE, SUCCESS }

    public BehaviorTree Tree { get; set; }
    
    // Constructor 
    public BTNode(BehaviorTree tree)
    {
        Tree = tree;
    }

    public virtual Result Execute()
    {
        return Result.FAILURE; 
        // Default value is FAILURE to override inheritance 
    }
}
