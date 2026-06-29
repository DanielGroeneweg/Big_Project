using UnityEditor;
using UnityEngine;

public class IdlePlayerState : State
{
    public IdlePlayerState(StatesData statesData)
    {
        data = statesData;
    }
    public override void Enter()
    {
        base.Enter();
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
        return true;
    }
    public bool ChangeWeaponToSpear()
    {
        return true;
    }
}
