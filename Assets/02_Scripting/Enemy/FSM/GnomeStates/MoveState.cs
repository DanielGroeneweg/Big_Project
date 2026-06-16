using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveState : State  
{
    public MoveState(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        // Set the move animation
        Debug.Log("Entered move state");
        if (data.target != null && data.enemyAgent.enabled && data.enemyAgent.isOnNavMesh)
            data.enemyAgent.SetDestination(data.target.position);
    }
    public override void Step()
    {
        base.Step();
        if (data.target != null && data.enemyAgent.enabled && data.enemyAgent.isOnNavMesh)
            data.enemyAgent.SetDestination(data.target.position);
    }

    public override void Exit()
    {
        // Set move animation to false
    }
    public bool TargetReached()
    {
        if (!data.enemyAgent.enabled || !data.enemyAgent.isOnNavMesh)
            return false;

        if (data.enemyAgent.pathPending)
            return false;

        return data.enemyAgent.remainingDistance <= data.enemyController.EnemyData.attackRange;
    }
    public bool TargetOutOfRange()
    {
        if (!data.enemyAgent.enabled || !data.enemyAgent.isOnNavMesh)
            return false;

        if (data.target == null)
            return true;

        if (data.enemyAgent.pathPending)
            return false;

        float dist = Vector3.Distance(data.enemyTransform.position, data.target.position);
        return dist > data.enemyController.EnemyData.detectionRange;
    }
}
