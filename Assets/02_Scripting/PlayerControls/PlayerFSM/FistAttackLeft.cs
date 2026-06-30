using UnityEngine;

public class FistAttackLeft : State
{

    private PlayerStatesData playerStatesData;
    public FistAttackLeft(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }
    public bool IsAttackOver()
    {
        return true;
    }

}
