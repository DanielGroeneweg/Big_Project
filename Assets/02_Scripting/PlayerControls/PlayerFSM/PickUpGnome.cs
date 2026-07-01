using UnityEngine;

public class PickUpGnome : State
{
    private PlayerStatesData playerStatesData;
    public PickUpGnome(PlayerStatesData data)
    {
        playerStatesData = data;
        this.data = data;
    }
    public override void Enter()
    {
        base.Enter();
        playerStatesData.ActivateWeaponVisual(WeaponType.None);
        playerStatesData.animator.SetTrigger("PickedUp");
    }

    public bool IsPickingUpGnome()
    {
        return playerStatesData.isPickingUpGnome;
    }
    public override void Exit()
    {
        base.Exit();
        playerStatesData.isPickingUpGnome = false;
    }
    public bool IsThrowingGnome() 
    {
        return playerStatesData.isThrowingGnome;
    }
}
