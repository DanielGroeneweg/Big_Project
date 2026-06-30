using UnityEngine;

public class IdleSpear : State
{
    private PlayerStatesData playerStatesData;
    public IdleSpear(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public bool Attack()
    {
        return playerStatesData.playerAttacked;
    }

    public bool ChangeToOneHandWeapon()
    {
        return true;
    }

    public bool ChangeToFists()
    {
        return true;
    }

}
