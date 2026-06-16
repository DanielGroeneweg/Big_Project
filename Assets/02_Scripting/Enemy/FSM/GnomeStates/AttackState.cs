using UnityEngine;
public class AttackState : State
{
    private float attackStartTime;
    public AttackState(StatesData statesData)
    {
        data = statesData;
    }
    public override void Enter()
    {
        base.Enter();
        attackStartTime = Time.time;
        Debug.Log("Entered attack state");

        data.weaponAnimator.Play("MeleeWeaponAttack");
        data.weaponAnimator.speed = 1f / data.enemyController.EnemyData.attackCountdown;
        data.weapon.Attack(data.enemyController.EnemyData.attackCountdown, data.enemyController.EnemyData.attackDamage);
    }
    public override void Exit()
    { 
        // Set attack animation to false
        // Already happens
    }
    public bool AttackOver()
    {
        return Time.time > attackStartTime + data.enemyController.EnemyData.attackCountdown;
    }
    public bool TargetStillInRange()
    {
        if (data.target == null)
            return false;

        return Vector3.Distance(data.enemyTransform.position, data.target.position)
               <= data.enemyController.EnemyData.attackRange;
    }
    public bool AttackOverAndTargetInRange()
    {
        return AttackOver() && TargetStillInRange();
    }

    public bool AttackOverAndTargetOutOfRange()
    {
        return AttackOver() && !TargetStillInRange();
    }
}