using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackState : State
{
    private float attackStartTime;
    public AttackState(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        attackStartTime = Time.time;
        Debug.Log("Entered attack state");
        // Set the attack animation
        // Enable attack collider
    }

    public override void Exit()
    { 
        // Set attack animation to false
    }
    public bool AttackOver()
    {
        return Time.time > attackStartTime + data.enemyController.EnemyData.attackCountdown;
    }
}
