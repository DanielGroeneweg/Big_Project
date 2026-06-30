using UnityEngine;

public class PlayerFSM : FSM
{
    public PlayerFSM(StatesData statesData)
    {
        data = statesData;
        
        var idle = new IdlePlayerState(data);
        var attackRightFist = new FistAttackRight(data);
        var attackLeftFist = new FistAttackLeft(data);
        var idleWeapon = new IdleOneHandWeapon(data);
        var attackWeapon1 = new AttackOneHandWeapon1(data);
        var attackWeapon2 = new AttackOneHandWeapon2(data);
        var spearIdle = new IdleSpear(data);
        var spearAttack = new AttackSpear(data);

        currentState = idle;

        idle.transitions.Add(new Transition(idle.Attack,ReturnRandomState(attackRightFist, attackLeftFist)));
        idle.transitions.Add(new Transition(idle.ChangeToOneHandWeapon, idleWeapon));
        idle.transitions.Add(new Transition(idle.ChangeWeaponToSpear, spearIdle));

        idleWeapon.transitions.Add(new Transition(idleWeapon.ToFists, idle));
        idleWeapon.transitions.Add(new Transition(idleWeapon.ToSpear, spearIdle));
        idleWeapon.transitions.Add(new Transition(idleWeapon.Attack, ReturnRandomState(attackWeapon1, attackWeapon2)));

        spearIdle.transitions.Add(new Transition(spearIdle.Attack, spearAttack));
        spearIdle.transitions.Add(new Transition(spearIdle.ChangeToOneHandWeapon, idleWeapon));
        spearIdle.transitions.Add(new Transition(spearIdle.ChangeToFists, idle));

        attackLeftFist.transitions.Add(new Transition(attackLeftFist.IsAttackOver, idle));
        attackRightFist.transitions.Add(new Transition(attackRightFist.IsAttackOver, idle));

        attackWeapon1.transitions.Add(new Transition(attackWeapon1.IsAttackOver, idleWeapon));
        attackWeapon2.transitions.Add(new Transition(attackWeapon2.IsAttackOver, idleWeapon));

        spearAttack.transitions.Add(new Transition(spearAttack.IsAttackOver, spearIdle));
    }

    public State ReturnRandomState(State state1,State state2)
    {
        int r = Random.Range(1, 3);
        if(r == 1)
        {
            return state1;
        }
        else
        {
            return state2;
        }
    }
}
