using UnityEngine;

public class AlignToState : State
{
    private Transform enemyTransform;
    private Vector3 direction;
    private float rotationSign;
    private EnemyStatesData enemyStatesData;
    public AlignToState(EnemyStatesData statesData)
    {
        data = statesData;
        enemyStatesData = statesData;
        this.enemyTransform = statesData.enemyTransform;
    }

    public override void Enter()
    {
        base.Enter();
        if (enemyStatesData.enemyAgent.enabled && enemyStatesData.enemyAgent.isOnNavMesh)
        {
            enemyStatesData.enemyAgent.ResetPath();
            enemyStatesData.enemyAgent.velocity = Vector3.zero; 
        }
        if (enemyStatesData.target != null)
            UpdateDirection(enemyStatesData.target.position);

    }
    public override void Step()
    {
        base.Step();

        if (enemyStatesData.target == null) return;

        Vector3 toTarget = (enemyStatesData.target.position - enemyTransform.position);
        toTarget.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);

        enemyTransform.rotation = Quaternion.RotateTowards(
            enemyTransform.rotation,
            targetRotation,
            enemyStatesData.enemyController.EnemyData.rotateSpeed * Time.deltaTime);
    }


    private void UpdateDirection(Vector3 targetPos)
    {  
        direction = (targetPos - enemyTransform.position).normalized;
        rotationSign = Mathf.Sign(Vector3.Dot(enemyTransform.right, direction));
       
    }

    public bool AlignedWithTarget()
    {
        if (enemyStatesData.target == null) return false;
        UpdateDirection(enemyStatesData.target.position);
        bool inRange = Vector3.Distance(enemyTransform.position, enemyStatesData.target.position)
                   <= enemyStatesData.enemyController.EnemyData.attackRange;
        bool facingTarget = Vector3.Dot(enemyTransform.forward, direction) > 0.9f; 
        return inRange && facingTarget;
    }

    public bool TargetOutOfRange()
    {
        if (!enemyStatesData.enemyAgent.enabled || !enemyStatesData.enemyAgent.isOnNavMesh) return false;
        if (enemyStatesData.enemyAgent.pathPending) return false;
        return enemyStatesData.enemyAgent.remainingDistance <= enemyStatesData.enemyAgent.stoppingDistance;
    }
}
