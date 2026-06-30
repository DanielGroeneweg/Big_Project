using UnityEngine;

public class AttackOneHandWeapon1 : State
{
    private PlayerStatesData playerStatesData;

    public AttackOneHandWeapon1(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }

    public bool IsAttackOver()
    {
        return true;
    }
}
