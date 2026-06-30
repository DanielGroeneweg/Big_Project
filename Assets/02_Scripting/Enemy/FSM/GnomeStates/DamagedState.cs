using UnityEngine;

public class DamagedState : State
{
    private float stunDuration = 0.8f; 
    private float timer;
    private EnemyStatesData enemyStatesData;
    public DamagedState(EnemyStatesData statesData)
    {
        data = statesData;
        enemyStatesData = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        //data.rb.AddForce()
        timer = 0f;

        if (enemyStatesData.enemyAgent.enabled)
            enemyStatesData.enemyAgent.isStopped = true;
        data.animator.SetTrigger("Damaged");
    }

    public override void Step()
    {
        base.Step();
        timer += Time.deltaTime;
    }

    public override void Exit()
    {
        base.Exit();

        enemyStatesData.isDamaged = false;

        if (enemyStatesData.enemyAgent.enabled)
            enemyStatesData.enemyAgent.isStopped = false;
    }

    public bool StunDamageDurationOver()
    {
        return timer >= stunDuration;
    }
    
}
