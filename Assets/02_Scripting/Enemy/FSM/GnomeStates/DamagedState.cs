using UnityEngine;

public class DamagedState : State
{
    private float stunDuration = 0.4f; 
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
        data.isDamaged = false;
        data.animator.SetTrigger("Damaged");
    }

    public override void Step()
    {
        base.Step();
        timer += Time.deltaTime;
    }

    public bool StunDamageDurationOver()
    {
        return timer >= stunDuration;
    }
    
}
