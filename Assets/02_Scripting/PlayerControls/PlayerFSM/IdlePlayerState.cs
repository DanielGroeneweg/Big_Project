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
        
        //set animations variables
    }
    
    public bool Attack()
    {
        return playerStatesData.playerAttacked;
    }
    public bool ChangeToOneHandWeapon()
    {
        
        if(playerStatesData.CurrentWeapon == WeaponType.Sickle || playerStatesData.CurrentWeapon == WeaponType.Club)
        {
            playerStatesData.animator.SetTrigger("IdleOneHand");
            return true;
        }
        return false;
    }
    public bool ChangeWeaponToSpear()
    {
        
        if(playerStatesData.CurrentWeapon == WeaponType.Spear)
        {
            playerStatesData.animator.SetTrigger("IdleSpear");
            return true;
        }
        return false;
    }
}
