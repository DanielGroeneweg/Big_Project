using UnityEngine;

/// <summary>
/// Abstract base class for building finite state machines (FSMs).
/// Inherits from State, allowing FSMs to be used as state themselves to build
/// sub-state machines.
/// </summary>
public abstract class FSM : State
{
    protected State currentState;

    public override void Step()
    {
        base.Step();
        currentState.Step();

        State nextState = currentState.NextState();

        if (nextState != null)
        {
            currentState.Exit();
            currentState = nextState;
            currentState.Enter();
        }
    }
}
