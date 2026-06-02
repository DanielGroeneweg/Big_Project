using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DieState : State
{
    public DieState(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        //Set the die animation


    }
}
