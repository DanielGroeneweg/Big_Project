using UnityEditor;
using UnityEngine;

public class IdlePlayerState : State
{
    private PlayerStatesData playerStatesData;
    public IdlePlayerState(PlayerStatesData statesData)
    {
        data = statesData;
        playerStatesData = statesData;
    }
    public override void Enter()
    {
        base.Enter();
        playerStatesData.ActivateWeaponVisual(WeaponType.None);
        playerStatesData.animator.SetInteger("Weapon", (int)WeaponType.None);
        //set animations variables
    }
    public override void Exit()
    {
        base.Exit();
        //set animations variables
    }
    public bool Attack()
    {
        return true;
    }
    public bool ChangeToOneHandWeapon()
    {
        return playerStatesData.CurrentWeapon == WeaponType.Sickle
            || playerStatesData.CurrentWeapon == WeaponType.Club;
    }
    public bool ChangeWeaponToSpear()
    {
        return playerStatesData.CurrentWeapon == WeaponType.Spear;
    }
}
