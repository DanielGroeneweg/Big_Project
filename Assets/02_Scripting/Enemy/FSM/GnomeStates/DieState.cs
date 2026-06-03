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
        Debug.Log("A gnome died.💀 Rest in peace my granny she got hit by a bazooka 😔 ");

    }
}
