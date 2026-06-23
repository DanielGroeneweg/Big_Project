using UnityEngine;

public class AlignToState : State
{
    private Transform enemyTransform;
    private Vector3 direction;
    private float rotationSign;
    public AlignToState(StatesData statesData)
    {
        data = statesData;
        this.enemyTransform = statesData.enemyTransform;
    }

    public override void Enter()
    {
        base.Enter();
        if (data.enemyAgent.enabled && data.enemyAgent.isOnNavMesh)
        {
            data.enemyAgent.ResetPath();
            data.enemyAgent.velocity = Vector3.zero; 
        }
        if (data.target != null)
            UpdateDirection(data.target.position);

    }
    public override void Step()
    {
        base.Step();

        if (data.target == null) return;

        Vector3 toTarget = (data.target.position - enemyTransform.position);
        toTarget.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);

        enemyTransform.rotation = Quaternion.RotateTowards(
            enemyTransform.rotation,
            targetRotation,
            data.enemyController.EnemyData.rotateSpeed * Time.deltaTime);
    }


    private void UpdateDirection(Vector3 targetPos)
    {  
        direction = (targetPos - enemyTransform.position).normalized;
        rotationSign = Mathf.Sign(Vector3.Dot(enemyTransform.right, direction));
       
    }

    public bool AlignedWithTarget()
    {
        if (data.target == null) return false;
        UpdateDirection(data.target.position);
        bool inRange = Vector3.Distance(enemyTransform.position, data.target.position)
                   <= data.enemyController.EnemyData.attackRange;
        bool facingTarget = Vector3.Dot(enemyTransform.forward, direction) > 0.9f; 
        return inRange && facingTarget;
    }

    public bool TargetOutOfRange()
    {
        if (!data.enemyAgent.enabled || !data.enemyAgent.isOnNavMesh) return false;
        if (data.enemyAgent.pathPending) return false;
        return data.enemyAgent.remainingDistance <= data.enemyAgent.stoppingDistance;
    }
}
