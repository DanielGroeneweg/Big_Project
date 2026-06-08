using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IdleState : State
{
    public IdleState(StatesData statesData)
    {
        data = statesData;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered idle state");
        if (data.enemyAgent.enabled && data.enemyAgent.isOnNavMesh)
            data.enemyAgent.ResetPath();
        // Set the idle animation
    }
    public override void Step()
    {
        base.Step();
        data.rb.linearVelocity = Vector3.zero;
        data.rb.angularVelocity = Vector3.zero;
    }

    public override void Exit()
    {
        // Set idle animation to false
    }
    /// <summary>
    /// Checks if the target is within the enemy's detection range.
    /// </summary>
    /// <returns>True if the target is in range, false otherwise.</returns>
    public bool IsTargetInRange()
    {
        if (data.target == null)
            return false;

        return Vector3.Distance(data.enemyTransform.position, data.target.position) <= data.enemyController.EnemyData.detectionRange;
    }
}
