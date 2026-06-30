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
        playerStatesData.animator.SetInteger("Weapon", (int)WeaponType.Spear);

    }

    public bool Attack()
    {
        return playerStatesData.playerAttacked;
    }

    public bool ChangeToOneHandWeapon()
    {
        return playerStatesData.CurrentWeapon == WeaponType.Sickle
            || playerStatesData.CurrentWeapon == WeaponType.Club;
    }

    public bool ChangeToFists()
    {
        return playerStatesData.CurrentWeapon == WeaponType.None;
    }

}
