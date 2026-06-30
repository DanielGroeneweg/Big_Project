using UnityEngine;

public class PickedUp : State
{
    private Collider enemyCollider;
    private EnemyStatesData enemyStates;

    public PickedUp(EnemyStatesData statesData)
    {
        data = statesData;
        enemyStates = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        enemyStates.enemyAgent.enabled = false; 
        enemyCollider = enemyStates.enemyController.GetComponent<Collider>();
        enemyCollider.enabled = false;
        enemyStates.SetKinematic(true);
        HealthBarEnabled(false);
        enemyStates.animator.SetTrigger("PickedUp");

    }
    public override void Step()
    {
        base.Step();
        if (!enemyStates.isPickedUp && enemyStates.grabGnome.IsGrounded())
        {
            enemyStates.enemyAgent.enabled = true;
        }
    }
    public override void Exit()
    {
        base.Exit();
        enemyStates.isStunned = true;
        enemyStates.SetKinematic(false);
        enemyCollider.enabled = true;
        HealthBarEnabled(true);
    }
    public bool WasThrown()
    {
        return !enemyStates.isPickedUp&&enemyStates.isLanded;
    }
    public void HealthBarEnabled(bool enabled)
    {
        foreach (var healthBar in enemyStates.enemyHealthBar)
        {
            healthBar.enabled = enabled;
        }
    }

}
