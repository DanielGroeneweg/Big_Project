using UnityEngine;

public class DamagedState : State
{
    public bool isStunDamagedOver =false;
    public DamagedState(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        isStunDamagedOver=true;
    }
    public override void Exit()
    {
        base.Exit();
        isStunDamagedOver=false;
    }

    public bool StunDamageDurationOver()
    {
        return isStunDamagedOver;
    }
    
}
