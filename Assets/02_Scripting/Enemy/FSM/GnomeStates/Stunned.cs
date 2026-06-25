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
        data.ignoreDamageState = true;
        if (data.enemyAgent.enabled && data.enemyAgent.isOnNavMesh)
        {
            data.SetAgentStopped(true);
            data.enemyAgent.ResetPath();
            data.enemyAgent.enabled = false;
        }
        originalDrag = data.rb.linearDamping;
        data.rb.linearDamping = 10f;
        data.animator.SetTrigger("Stunned");
        data.enemyController.health.Damage(10f);
        data.isDamaged = false;
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
        data.ignoreDamageState = false;
        data.SetAgentStopped(false);
        data.enemyAgent.enabled = true;

        stunTimer = 0f;
        data.rb.linearDamping = originalDrag;
        data.attackCollider.transform.localPosition = data.colliderLocalPosition;
        data.attackCollider.transform.localRotation = data.colliderLocalRotation;

    }

    public bool StunOver()
    {
        return !data.isStunned;
    }
}
