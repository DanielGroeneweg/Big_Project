using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class GnomeFSM : FSM
{
    public GnomeFSM(StatesData statesData)
    {
        data = statesData;

        var idle = new IdleState(statesData);
        var move = new MoveState(statesData);
        var align = new AlignToState(statesData);
        var attack = new AttackState(statesData);
        var pickedUp = new PickedUp(statesData);
        var death = new DieState(statesData);

        currentState = idle;

        idle.transitions.Add(new Transition(idle.IsTargetInRange, move));
        move.transitions.Add(new Transition(move.TargetReached, align));
        align.transitions.Add(new Transition(align.AlignedWithTarget, attack));

        align.transitions.Add(new Transition(align.TargetOutOfRange, move));
        move.transitions.Add(new Transition(move.TargetOutOfRange, idle));


        attack.transitions.Add(new Transition(attack.AttackOverAndTargetInRange, align));
        attack.transitions.Add(new Transition(attack.AttackOverAndTargetOutOfRange, idle));

        Func<bool> isPickedUp = () => data.isPickedUp;

        idle.transitions.Add(new Transition(isPickedUp, pickedUp));
        move.transitions.Add(new Transition(isPickedUp, pickedUp));
        align.transitions.Add(new Transition(isPickedUp, pickedUp));
        attack.transitions.Add(new Transition(isPickedUp, pickedUp));

        pickedUp.transitions.Add(new Transition(pickedUp.WasThrown, idle));

        idle.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        move.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        attack.transitions.Add(new Transition(() => data.enemyController.IsDead, death));  
        align.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        pickedUp.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
    }

    
}
