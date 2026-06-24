using UnityEngine;
public class AttackState : State
{
    private float attackStartTime;
    private float cooldownTimer;
    private bool isOnCooldown;

    public AttackState(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        attackStartTime = Time.time;
        isOnCooldown = false;
        cooldownTimer = 0f;
        Debug.Log("Entered attack state");
        data.animator.SetTrigger("Attack");
        data.weapon.Attack(data.enemyController.EnemyData.attackCountdown, data.enemyController.EnemyData.attackDamage);
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
            if (cooldownTimer < data.enemyController.EnemyData.attackCooldown)
            {
                cooldownTimer += Time.deltaTime;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
  
    }

    public bool AttackOver()
    {
        return Time.time > attackStartTime + data.enemyController.EnemyData.attackCountdown;
    }

    public bool CooldownOver()
    {
        return isOnCooldown && cooldownTimer >= data.enemyController.EnemyData.attackCooldown;
    }

    public bool TargetStillInRange()
    {
        if (data.target == null)
            return false;

        return Vector3.Distance(data.enemyTransform.position, data.target.position)
               <= data.enemyController.EnemyData.attackRange;
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