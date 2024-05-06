using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BTRepeater : BTDecorator
{
    public BTRepeater (BehaviorTree tree, BTNode child) : base(tree, child)
    {
    }
    // Repeater will always repeat
    public override Result Execute()
    {
        Debug.Log("Child returned: " + Child.Execute());
        return Result.RUNNING;
    }
}
