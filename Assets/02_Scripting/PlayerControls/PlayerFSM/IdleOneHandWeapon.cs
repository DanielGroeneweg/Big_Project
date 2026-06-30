using UnityEngine;

public class IdleOneHandWeapon : State
{
    private PlayerStatesData playerStatesData;
    public IdleOneHandWeapon(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
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
