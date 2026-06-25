using System.Collections;
using UnityEngine;
public class MoveState : State  
{
    bool canPlayStepSound = true;
    float time;
    float timePassed;
    public MoveState(StatesData statesData)
    {
        data = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        // Set the move animation
        if (data.target != null && data.enemyAgent.enabled && data.enemyAgent.isOnNavMesh)
            data.enemyAgent.SetDestination(data.target.position);
        data.animator.SetBool("Move", true);
    }
    public override void Step()
    {
        base.Step();
        if (data.target != null && data.enemyAgent.enabled && data.enemyAgent.isOnNavMesh)
            data.enemyAgent.SetDestination(data.target.position);

        CheckSound();
        
        if (canPlayStepSound)
        {
            AudioClip clip = data.footSteps[Random.Range(0, data.footSteps.Length)];
            SoundManager.instance.PlaySound(clip, data.enemyController.transform.position, true);
            canPlayStepSound = false;
            time = clip.length;
            timePassed = 0;
        }
    }

    public override void Exit()
    {
        // Set move animation to false
        base.Exit();
        data.animator.SetBool("Move", false);
    }
    public bool TargetReached()
    {
        if (!data.enemyAgent.enabled || !data.enemyAgent.isOnNavMesh)
            return false;

        if (data.target == null)
            return false;

        if (data.enemyAgent.pathPending)
            return false;

        float dist = Vector3.Distance(
            data.enemyTransform.position,
            data.target.position
        );

        return dist <= data.enemyController.EnemyData.attackRange;
    }
    public bool TargetOutOfRange()
    {
        if (!data.enemyAgent.enabled || !data.enemyAgent.isOnNavMesh)
            return false;

        if (data.target == null)
            return true;

        if (data.enemyAgent.pathPending)
            return false;

        float dist = Vector3.Distance(data.enemyTransform.position, data.target.position);
        return dist > data.enemyController.EnemyData.detectionRange;
    }
    private void CheckSound()
    {
        if (canPlayStepSound) return;

        timePassed += Time.deltaTime;
        if (timePassed >= time)
            canPlayStepSound = true;
    }
}
