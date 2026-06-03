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
        if (data.target != null)
            data.enemyAgent.SetDestination(data.target.position);
    }

    public override void Step()
    {
        base.Step();
        Vector3 dir = (data.target.position - data.enemyTransform.position).normalized;
        dir.y = 0;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        data.enemyTransform.rotation = Quaternion.Slerp(data.enemyTransform.rotation, lookRot, Time.deltaTime * 5f);
        if (data.target != null)
            data.enemyAgent.SetDestination(data.target.position);
    }
    public override void Exit()
    {
        // Set move animation to false
    }
    public bool TargetReached()
    {
        if (data.enemyAgent.pathPending)
            return false;

        return data.enemyAgent.remainingDistance <= data.enemyController.EnemyData.attackRange;
    }
    public bool TargetOutOfRange()
    {
        if (data.target == null)
            return true;

        if (data.enemyAgent.pathPending)
            return false;

        float dist = Vector3.Distance(data.enemyTransform.position, data.target.position);
        return dist > data.enemyController.EnemyData.detectionRange;
    }
}
