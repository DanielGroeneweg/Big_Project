using UnityEngine;

public class Stunned : State
{

    private float stunTimer;
    private float originalDrag;
    public Stunned(StatesData statesData)
    {
       data = statesData;
    }
    public override void Enter()
    {
        if (data.enemyAgent.enabled && data.enemyAgent.isOnNavMesh)
        {
            base.data.SetAgentStopped(true);
            data.enemyAgent.ResetPath();
        }
        data.rb.linearVelocity = Vector3.zero;
        data.rb.angularVelocity = Vector3.zero;
        originalDrag = data.rb.linearDamping;
        data.rb.linearDamping = 10f;
        Debug.Log("Stunned");
        //statesData.animator.SetBool("isStunned", true);
    }
    public override void Step()
    {
        base.Step();
        if(stunTimer < data.stunDuration)
        {
            stunTimer += Time.deltaTime;
        }
        else
        {
            data.isStunned = false;
            stunTimer = 0f;
        }
    }
    public override void Exit()
    {
        base.data.SetAgentStopped(false);
        stunTimer = 0f;
        data.rb.linearDamping = originalDrag;
    }

    public bool StunOver()
    {
        return !data.isStunned;
    }
}
