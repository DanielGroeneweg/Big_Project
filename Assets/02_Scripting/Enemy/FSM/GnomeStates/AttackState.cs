using UnityEngine;
public class AttackState : State
{
    private float attackStartTime;
    private float cooldownTimer;
    private bool isOnCooldown;
    private EnemyStatesData enemyStates;
    public AttackState(EnemyStatesData statesData)
    {
        data = statesData;
        enemyStates = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        attackStartTime = Time.time;
        isOnCooldown = false;
        cooldownTimer = 0f;
        enemyStates.animator.SetTrigger("Attack");
        enemyStates.animator.speed = enemyStates.attackAnimatorSpeed;
        enemyStates.weapon.Attack(enemyStates.enemyController.EnemyData.attackDuration, enemyStates.enemyController.EnemyData.attackDamage);
    }

    public override void Step()
    {
        base.Step();

        if (!isOnCooldown && AttackOver())
        {
            isOnCooldown = true;
           
        }

        if (isOnCooldown)
        {
            if (cooldownTimer < enemyStates.enemyController.EnemyData.attackCooldown)
            {
                cooldownTimer += Time.deltaTime;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemyStates.animator.speed = 1f;
    }

    public bool AttackOver()
    {
        return Time.time > attackStartTime + enemyStates.enemyController.EnemyData.attackDuration;
    }

    public bool CooldownOver()
    {
        return isOnCooldown && cooldownTimer >= enemyStates.enemyController.EnemyData.attackCooldown;
    }

    public bool TargetStillInRange()
    {
        if (enemyStates.target == null)
            return false;

        return Vector3.Distance(enemyStates.enemyTransform.position, enemyStates.target.position)
               <= enemyStates.enemyController.EnemyData.attackRange;
    }

    public bool CooldownOverAndTargetInRange()
    {
        return CooldownOver() && TargetStillInRange();
    }

    public bool CooldownOverAndTargetOutOfRange()
    {
        return CooldownOver() && !TargetStillInRange();
    }
}