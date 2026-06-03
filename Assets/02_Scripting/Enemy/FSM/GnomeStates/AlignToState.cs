using UnityEditor.Experimental.GraphView;
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
        Debug.Log("Entered align to state");
        if(data.target != null)
        {
            UpdateDirection(data.target.position);
        }
            
    }
    public override void Step()
    {
        base.Step();

        if (data.target != null)
        {
            UpdateDirection(data.target.position);
        }

        enemyTransform.Rotate(
            enemyTransform.up,
            rotationSign * data.enemyController.EnemyData.rotateSpeed * Time.deltaTime
        );
    }


    private void UpdateDirection(Vector3 targetPos)
    {
        direction = (targetPos - enemyTransform.position).normalized;
        rotationSign = Mathf.Sign(Vector3.Dot(enemyTransform.position, direction));
    }

    public bool AlignedWithTarget()
    {
        return Vector3.Dot(enemyTransform.forward, direction) >= 0.95f;
    }

    public bool TargetOutOfRange()
    {
        if (data.target == null)
            return true;

        return Vector3.Distance(enemyTransform.position, data.target.position) > data.enemyController.EnemyData.attackRange;
    }
}
