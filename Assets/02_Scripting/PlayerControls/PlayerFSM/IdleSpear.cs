using UnityEngine;

public class IdleSpear : State
{
    private PlayerStatesData playerStatesData;
    public IdleSpear(PlayerStatesData data)
    {
        this.data = data;
        playerStatesData = data;
    }

    public override void Enter()
    {
        base.Enter();
        playerStatesData.ActivateWeaponVisual(WeaponType.Spear);
        

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

    public bool ChangeToFists()
    {
        
        if(playerStatesData.CurrentWeapon == WeaponType.None)
        {
            playerStatesData.animator.SetTrigger("IdleFist");
            return true;
        }
        return false;
    }

}
