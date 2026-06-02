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
        Debug.Log("Entered idle state");
        // Set the idle animation
    }

    public override void Exit()
    {
        // Set idle animation to false
    }

    public bool IsTargetInRange()
    {
        if (data.target == null)
            return false;

        return Vector3.Distance(data.enemyTransform.position, data.target.position) <= data.enemyController.EnemyData.detectionRange;
    }
}
