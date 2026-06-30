using UnityEngine;

public class AttackOneHandWeapon2 : State
{
    private PlayerStatesData playerStatesData;
    private float attackStartTime;
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
        //return Time.time > attackStartTime + playerStatesData.animator.
        //add Later after hands
    }
    public override void Exit()
    {
        base.Exit();
        playerStatesData.playerAttacked = false;
    }
}
