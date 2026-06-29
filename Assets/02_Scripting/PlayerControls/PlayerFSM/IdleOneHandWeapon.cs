using UnityEngine;

public class IdleOneHandWeapon : State
{

    public IdleOneHandWeapon(StatesData data)
    {
        this.data = data;
    }

    public bool ToFists()
    {
        return true;
    }
    public bool ToSpear()
    {
        return true;
    }

    public bool Attack()
    {
        return true;
    }
    
}
