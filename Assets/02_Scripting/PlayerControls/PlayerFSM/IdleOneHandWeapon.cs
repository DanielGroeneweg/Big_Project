using UnityEngine;

public class IdleOneHandWeapon : State
{
    private PlayerStatesData playerStatesData;
    public IdleOneHandWeapon(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }
    public override void Enter()
    {
        base.Enter();
        playerStatesData.ActivateWeaponVisual(playerStatesData.CurrentWeapon);
        
    }

    public bool ToFists()
    {
        
        if(playerStatesData.CurrentWeapon == WeaponType.None)
        {
            playerStatesData.animator.SetTrigger("IdleFist");
            return true;
        }
        return false;
    }
    public bool ToSpear()
    {
        
        if(playerStatesData.CurrentWeapon == WeaponType.Spear)
        {
            playerStatesData.animator.SetTrigger("IdleSpear");
            return true;
        }
        return false;
    }

    public bool Attack()
    {
        return playerStatesData.playerAttacked;
    }
    
}
