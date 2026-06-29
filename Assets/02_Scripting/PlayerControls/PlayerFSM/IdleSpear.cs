using UnityEngine;

public class IdleSpear : State
{
    public IdleSpear(StatesData data)
    {
        this.data = data;
    }

    public bool Attack()
    {
        return true;
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
