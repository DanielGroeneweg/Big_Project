using UnityEngine;

public class AttackOneHandWeapon1 : State
{
    private PlayerStatesData playerStatesData;

    public AttackOneHandWeapon1(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }
    public override void Enter()
    {
        base.Enter();
        playerStatesData.animator.SetTrigger("AttackUp");
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
