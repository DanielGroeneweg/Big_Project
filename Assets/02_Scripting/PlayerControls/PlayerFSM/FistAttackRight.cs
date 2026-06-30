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

    }
    public bool IsAttackOver()
    {
        return true;
    }
    
}
