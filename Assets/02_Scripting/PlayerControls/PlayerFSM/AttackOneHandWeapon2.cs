using UnityEngine;

public class AttackOneHandWeapon2 : State
{
    private PlayerStatesData playerStatesData;
    public AttackOneHandWeapon2(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }
    public override void Enter()
    {
        base.Enter();
        playerStatesData.animator.SetTrigger("AttackSideways");
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
