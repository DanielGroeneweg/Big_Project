using UnityEngine;

public class AttackSpear : State
{
    private PlayerStatesData playerStatesData;
    public AttackSpear(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }
    public override void Enter()
    {
        base.Enter();
        playerStatesData.animator.SetTrigger("SpearAttack");
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
