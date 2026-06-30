using UnityEngine;

public class AttackOneHandWeapon2 : State
{
    public AttackOneHandWeapon2(StatesData data)
    {
        this.data = data;
    }
    public bool IsAttackOver()
    {
        return true;
    }
}
