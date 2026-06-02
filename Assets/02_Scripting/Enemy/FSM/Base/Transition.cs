using System;

[Serializable]
/// <summary>
/// Represents a transition between two states in the FSM.
/// Holds a condition that determines when the transition should occur,
/// and the state to transition to when the condition is true.
/// </summary>
public class Transition
{
    /// <summary>
    /// A function delegate that returns true when the transition condition is met.
    /// This is evaluated in Step() by states to check if the FSM should switch states.
    /// </summary>
    public Func<bool> condition;

    /// <summary>
    /// The state to transition to if the condition is true.
    /// </summary>
    public State nextState;

    /// <summary>
    /// Constructor to create a new transition.
    /// </summary>
    /// <param name="pCondition">The condition function to evaluate.</param>
    /// <param name="pNextState">The next state to transition to if condition is met.</param>
    public Transition(Func<bool> pCondition, State pNextState)
    {
        condition = pCondition;
        nextState = pNextState;
    }
}
