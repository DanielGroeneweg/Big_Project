using UnityEngine;

public class ThrowGnome : State
{
    private PlayerStatesData playerStatesData;
    private float timer;
    private float throwDuration = 0.75f;
    public ThrowGnome(PlayerStatesData data)
    {
        playerStatesData = data;
        this.data = data;
    }
    public override void Enter()
    {
        base.Enter();
        playerStatesData.animator.SetTrigger("Throw");
    }
    
    public override void Step()
    {
        base.Step();
        timer += Time.deltaTime;
    }

    public override void Exit()
    {
        base.Exit();
        playerStatesData.isThrowingGnome = false;
    }

    public bool ChangeToOneHandWeapon()
    {

        if (IsThrowAnimationOver() && (playerStatesData.CurrentWeapon == WeaponType.Sickle || playerStatesData.CurrentWeapon == WeaponType.Club))
        {
            playerStatesData.animator.SetTrigger("IdleOneHand");
            return true;
        }
        return false;
    }
    public bool ChangeWeaponToSpear()
    {

        if (IsThrowAnimationOver() && playerStatesData.CurrentWeapon == WeaponType.Spear)
        {
            playerStatesData.animator.SetTrigger("IdleSpear");
            return true;
        }
        return false;
    }
    public bool ToFists()
    {

        if (IsThrowAnimationOver() && playerStatesData.CurrentWeapon == WeaponType.None)
        {
            playerStatesData.animator.SetTrigger("IdleFist");
            return true;
        }
        return false;
    }
    private bool IsThrowAnimationOver()
    {
        return timer >= throwDuration;
    }
}
