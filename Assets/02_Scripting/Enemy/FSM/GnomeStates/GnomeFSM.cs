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
        var death = new DieState(statesData);

        currentState = idle;

        idle.transitions.Add(new Transition(idle.IsTargetInRange, move));
        move.transitions.Add(new Transition(move.TargetReached, align));
        align.transitions.Add(new Transition(align.AlignedWithTarget, attack));

        attack.transitions.Add(new Transition(attack.AttackOver, idle));
        align.transitions.Add(new Transition(align.TargetOutOfRange, move));
        move.transitions.Add(new Transition(move.TargetOutOfRange, idle));

        idle.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        move.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        attack.transitions.Add(new Transition(() => data.enemyController.IsDead, death));  
        align.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
    }

    
}
