using UnityEngine;

public class IdleState : State
{
    private EnemyStatesData enemyData;
    public IdleState(EnemyStatesData statesData)
    {
        this.data = statesData;     
        enemyData = statesData;
    }
    public override void Enter()
    {
        base.Enter();
        if (enemyData.enemyAgent.enabled && enemyData.enemyAgent.isOnNavMesh)
            enemyData.enemyAgent.ResetPath();
        data.animator.SetBool("Idle", true);
        // Set the idle animation
    }
    public override void Step()
    {
        base.Step();
        enemyData.rb.linearVelocity = Vector3.zero;
        enemyData.rb.angularVelocity = Vector3.zero;
    }

    public override void Exit()
    {
        // Set idle animation to false
        data.animator.SetBool("Idle", false);
    }
    /// <summary>
    /// Checks if the target is within the enemy's detection range.
    /// </summary>
    /// <returns>True if the target is in range, false otherwise.</returns>
    public bool IsTargetInRange()
    {
        if (enemyData.target == null)
            return false;

        return Vector3.Distance(enemyData.enemyTransform.position, enemyData.target.position) <= enemyData.enemyController.EnemyData.detectionRange;
    }
}
