using UnityEngine;

public class DamagedState : State
{
    private float stunDuration = 0.8f; 
    private float timer;
    public DamagedState(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        //data.rb.AddForce()
        timer = 0f;

        if (data.enemyAgent.enabled)
            data.enemyAgent.isStopped = true;
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

        data.isDamaged = false;

        if (data.enemyAgent.enabled)
            data.enemyAgent.isStopped = false;
    }

    public bool StunDamageDurationOver()
    {
        return timer >= stunDuration;
    }
    
}
