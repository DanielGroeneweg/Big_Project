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
        var stunned = new Stunned(statesData);
        var damaged = new DamagedState(statesData);
        var death = new DieState(statesData);

        Func<bool> isPickedUp = () => data.isPickedUp;
        Func<bool> isStunned = () => data.isStunned;
        Func<bool> isDamaged = () => data.isDamaged;

        currentState = idle;

        idle.transitions.Add(new Transition(idle.IsTargetInRange, move));
        move.transitions.Add(new Transition(move.TargetReached, align));
        align.transitions.Add(new Transition(align.AlignedWithTarget, attack));

        align.transitions.Add(new Transition(align.TargetOutOfRange, move));
        move.transitions.Add(new Transition(move.TargetOutOfRange, idle));

        attack.transitions.Add(new Transition(attack.CooldownOverAndTargetInRange, align));
        attack.transitions.Add(new Transition(attack.CooldownOverAndTargetOutOfRange, idle));

        idle.transitions.Add(new Transition(isPickedUp, pickedUp));
        move.transitions.Add(new Transition(isPickedUp, pickedUp));
        align.transitions.Add(new Transition(isPickedUp, pickedUp));
        attack.transitions.Add(new Transition(isPickedUp, pickedUp));
        stunned.transitions.Add(new Transition(isPickedUp,pickedUp));

        pickedUp.transitions.Add(new Transition(pickedUp.WasThrown, stunned));

        idle.transitions.Add(new Transition(isStunned, stunned));
        move.transitions.Add(new Transition(isStunned, stunned));
        align.transitions.Add(new Transition(isStunned, stunned));
        attack.transitions.Add(new Transition(isStunned, stunned));

        stunned.transitions.Add(new Transition(stunned.StunOver, idle));

        idle.transitions.Add(new Transition(isDamaged, damaged));
        move.transitions.Add(new Transition(isDamaged, damaged));
        align.transitions.Add(new Transition(isDamaged, damaged));
        attack.transitions.Add(new Transition(isDamaged, damaged));

        damaged.transitions.Add(new Transition(() => damaged.StunDamageDurationOver() && data.target != null, move));

        damaged.transitions.Add(new Transition(() => damaged.StunDamageDurationOver() && data.target == null, idle));

        idle.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        move.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        attack.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        align.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        pickedUp.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
        stunned.transitions.Add(new Transition(() => data.enemyController.IsDead, death));
    }


}