using UnityEngine;

public class Stunned : State
{
    private float stunTimer;
    private float originalDrag;
    private EnemyStatesData enemyStates;
    public Stunned(EnemyStatesData statesData)
    {
        data = statesData;
        enemyStates = statesData;
    }
    public override void Enter()
    {
        enemyStates.ignoreDamageState = true;
        if (enemyStates.enemyAgent.enabled && enemyStates.enemyAgent.isOnNavMesh)
        {
            enemyStates.SetAgentStopped(true);
            enemyStates.enemyAgent.ResetPath();
            enemyStates.enemyAgent.enabled = false;
        }
        originalDrag = enemyStates.rb.linearDamping;
        enemyStates.rb.linearDamping = 10f;
        enemyStates.animator.SetTrigger("Stunned");
        enemyStates.enemyController.health.Damage(10f);
        enemyStates.isDamaged = false;
        //statesData.animator.SetBool("isStunned", true);
    }
    public override void Step()
    {
        base.Step();
        if(stunTimer < enemyStates.stunDuration)
        {
            stunTimer += Time.deltaTime;
           
        }
        else
        {
            enemyStates.isStunned = false;
            stunTimer = 0f;
        }
    }
    public override void Exit()
    {
        enemyStates.ignoreDamageState = false;
        enemyStates.SetAgentStopped(false);
        enemyStates.enemyAgent.enabled = true;

        stunTimer = 0f;
        enemyStates.rb.linearDamping = originalDrag;
        enemyStates.attackCollider.transform.localPosition = enemyStates.colliderLocalPosition;
        enemyStates.attackCollider.transform.localRotation = enemyStates.colliderLocalRotation;

    }

    public bool StunOver()
    {
        return !enemyStates.isStunned;
    }
}
