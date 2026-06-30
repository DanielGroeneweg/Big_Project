using UnityEngine;

public class AttackOneHandWeapon2 : State
{
    private PlayerStatesData playerStatesData;
    public AttackOneHandWeapon2(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }
    public bool IsAttackOver()
    {
        return true;
    }
}
