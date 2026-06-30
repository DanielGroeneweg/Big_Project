using UnityEngine;

public class AttackSpear : State
{
    private PlayerStatesData playerStatesData;
    public AttackSpear(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }

    public bool IsAttackOver()
    {
        return true;
    }
}
