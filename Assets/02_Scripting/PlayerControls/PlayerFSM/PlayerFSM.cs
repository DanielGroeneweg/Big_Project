using UnityEngine;

public class PlayerFSM : FSM
{
    public PlayerFSM(PlayerStatesData statesData)
    {
        data = statesData;
        PlayerStatesData playerStatesData = statesData;
        
        var idle = new IdlePlayerState(playerStatesData);
        var attackRightFist = new FistAttackRight(playerStatesData);
        var attackLeftFist = new FistAttackLeft(playerStatesData);
        var idleWeapon = new IdleOneHandWeapon(playerStatesData);
        var attackWeapon1 = new AttackOneHandWeapon1(playerStatesData);
        var attackWeapon2 = new AttackOneHandWeapon2(playerStatesData);
        var spearIdle = new IdleSpear(playerStatesData);
        var spearAttack = new AttackSpear(playerStatesData);
        var pickUpGnome = new PickUpGnome(playerStatesData);
        var throwGnnome = new ThrowGnome(playerStatesData);

        currentState = idle;

        idle.transitions.Add(new Transition(idle.Attack, () => ReturnRandomState(attackRightFist, attackLeftFist)));
        idle.transitions.Add(new Transition(idle.ChangeToOneHandWeapon, idleWeapon));
        idle.transitions.Add(new Transition(idle.ChangeWeaponToSpear, spearIdle));

        idleWeapon.transitions.Add(new Transition(idleWeapon.ToFists, idle));
        idleWeapon.transitions.Add(new Transition(idleWeapon.ToSpear, spearIdle));
        idleWeapon.transitions.Add(new Transition(idleWeapon.Attack, () => ReturnRandomState(attackWeapon1, attackWeapon2)));

        spearIdle.transitions.Add(new Transition(spearIdle.Attack, spearAttack));
        spearIdle.transitions.Add(new Transition(spearIdle.ChangeToOneHandWeapon, idleWeapon));
        spearIdle.transitions.Add(new Transition(spearIdle.ChangeToFists, idle));

        attackLeftFist.transitions.Add(new Transition(attackLeftFist.IsAttackOver, idle));
        attackRightFist.transitions.Add(new Transition(attackRightFist.IsAttackOver, idle));

        attackWeapon1.transitions.Add(new Transition(attackWeapon1.IsAttackOver, idleWeapon));
        attackWeapon2.transitions.Add(new Transition(attackWeapon2.IsAttackOver, idleWeapon));

        spearAttack.transitions.Add(new Transition(spearAttack.IsAttackOver, spearIdle));

        idle.transitions.Add(new Transition(pickUpGnome.IsPickingUpGnome, pickUpGnome));
        idleWeapon.transitions.Add(new Transition(pickUpGnome.IsPickingUpGnome, pickUpGnome));
        spearIdle.transitions.Add(new Transition(pickUpGnome.IsPickingUpGnome, pickUpGnome));

        pickUpGnome.transitions.Add(new Transition(pickUpGnome.IsThrowingGnome, throwGnnome));

        throwGnnome.transitions.Add(new Transition(throwGnnome.ToFists, idle));
        throwGnnome.transitions.Add(new Transition(throwGnnome.ChangeToOneHandWeapon, idleWeapon));
        throwGnnome.transitions.Add(new Transition(throwGnnome.ChangeWeaponToSpear, spearIdle));

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
