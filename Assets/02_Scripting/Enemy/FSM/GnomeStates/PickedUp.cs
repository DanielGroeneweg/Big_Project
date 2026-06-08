using Unity.VisualScripting;
using UnityEngine;

public class PickedUp : State
{
    private bool thrown =false;
    private float throwTimer = 0f;
    public PickedUp(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered PickedUp state");
        data.enemyAgent.enabled = false; 
        data.rb.isKinematic = true;
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
        //thrown = false;
        //throwTimer = 0f;
        //data.enemyAgent.enabled = true;
        //data.rb.isKinematic = false;
        data.rb.isKinematic = false;
    }
    public bool WasThrown()
    {
        return !data.isPickedUp&&data.isLanded;
    }

}
