using UnityEngine;

public class BTDecorator : BTNode
{
    // Set the child property to the child when constructed
    public BTNode Child { get; set; } 
    public BTDecorator(BehaviorTree tree, BTNode child) : base(tree)
    {
        Child = child;
    }
}
