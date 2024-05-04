using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Behavior Trees in Unity using C#
// https://youtu.be/aVf3awPrVPE?si=xdIdi_0WTv_SQKl_
// Current Time = 13:22
public class BehaviorTree : MonoBehaviour
{
    private BTNode mRoot;
    private bool startedBehavior; // Checks if the behaviour has started
    private Coroutine behavior;

    // Dictionary used as a blackboard - BT memory
    public Dictionary<string, object> Blackboard {  get; set; } 
    public BTNode Root {  get { return mRoot; } } // Getter for the root

    private void Start()
    {
        Blackboard = new Dictionary<string, object>();
        Blackboard.Add("WorldBounds", new Rect (0,0,5,5));

        // Stop initial behaviour
        startedBehavior = false;
        // Allows access from other nodes
        mRoot = new BTNode(this);
    }

    private void Update()
    {
        if (!startedBehavior) 
        {
            behavior = StartCoroutine(RunBehavior());
            startedBehavior = true;
        }
    }

    private IEnumerator RunBehavior()
    {
        BTNode.Result result = Root.Execute();
        while (result == BTNode.Result.RUNNING)
        {
            Debug.Log("Root Result: " + result);
            yield return null;
            result = Root.Execute();
        }

        Debug.Log("Behaviour has finished with: " + result);
    }
}
