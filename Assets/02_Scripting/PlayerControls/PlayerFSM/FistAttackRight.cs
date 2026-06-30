using UnityEngine;

public class FistAttackRight : State
{
    private PlayerStatesData playerStatesData;

    public FistAttackRight(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }

    public override void Enter()
    {
        base.Enter();
        playerStatesData.animator.SetTrigger("RightFist");

    }
    public bool IsAttackOver()
    {
        return true;
    }
    public override void Exit()
    {
        playerStatesData.playerAttacked = false;
        base.Exit();
    }
    
}
