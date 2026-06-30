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
        return playerStatesData.CurrentWeapon == WeaponType.None;
    }
    public bool ToSpear()
    {
        return playerStatesData.CurrentWeapon == WeaponType.Spear;
    }

    public bool Attack()
    {
        return playerStatesData.playerAttacked;
    }
    
}
