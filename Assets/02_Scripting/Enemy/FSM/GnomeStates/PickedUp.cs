using UnityEditor.XR;
using UnityEngine;

public class PickedUp : State
{
    private Collider enemyCollider;

    public PickedUp(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered PickedUp state");
        data.enemyAgent.enabled = false; 
        enemyCollider = data.enemyController.GetComponent<Collider>();
        enemyCollider.enabled = false;
        data.SetKinematic(true);
        HealthBarEnabled(false);
        data.animator.SetTrigger("PickedUp");

    }
    public override void Step()
    {
        base.Step();
        if (!data.isPickedUp && data.grabGnome.IsGrounded())
        {
            data.enemyAgent.enabled = true;
        }
    }
    public override void Exit()
    {
        base.Exit();
        data.isStunned = true;
        data.SetKinematic(false);
        enemyCollider.enabled = true;
        HealthBarEnabled(true);
    }
    public bool WasThrown()
    {
        return !data.isPickedUp&&data.isLanded;
    }
    public void HealthBarEnabled(bool enabled)
    {
        foreach (var healthBar in data.enemyHealthBar)
        {
            healthBar.enabled = enabled;
        }
    }

}
