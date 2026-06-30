using UnityEngine;

public class FistAttackLeft : State
{

    private PlayerStatesData playerStatesData;
    public FistAttackLeft(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }
    public override void Enter()
    {
        base.Enter();
        playerStatesData.animator.SetTrigger("LeftFist");
    }
    public bool IsAttackOver()
    {
        return true;
    }
    public override void Exit()
    {
        base.Exit();
        playerStatesData.playerAttacked = false;
    }

}
