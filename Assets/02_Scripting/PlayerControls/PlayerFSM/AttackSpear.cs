using UnityEngine;

public class AttackSpear : State
{

    public AttackSpear(StatesData data)
    {
        this.data = data;
    }

    public bool IsAttackOver()
    {
        return true;
    }
}
