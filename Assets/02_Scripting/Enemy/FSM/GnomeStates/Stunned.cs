using UnityEngine;

public class Stunned : State
{
    private StatesData statesData;
    private float stunTimer;

    public Stunned(StatesData statesData)
    {
        this.statesData = statesData;
    }
    public override void Enter()
    {
        statesData.enemyAgent.isStopped = true;
        statesData.enemyAgent.ResetPath();
        Debug.Log("Stunned");
        //statesData.animator.SetBool("isStunned", true);
    }
    public override void Step()
    {
        base.Step();
        if(stunTimer < statesData.stunDuration)
        {
            stunTimer += Time.deltaTime;
        }
        else
        {
            statesData.isStunned = false;
            stunTimer = 0f;
        }
    }
    public override void Exit()
    {
        statesData.enemyAgent.isStopped = false;
        stunTimer = 0f;
    }

    public bool IsStunned()
    {
        return statesData.isStunned;
    }
}
