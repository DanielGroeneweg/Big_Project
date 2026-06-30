using System.Collections;
using UnityEngine;
public class MoveState : State  
{
    bool canPlayStepSound = true;
    float time;
    float timePassed;
    private EnemyStatesData enemyStates;
    public MoveState(EnemyStatesData statesData)
    {
        data = statesData;
        enemyStates = statesData;
    }

    public override void Enter()
    {
        base.Enter();
        // Set the move animation
        if (enemyStates.target != null && enemyStates.enemyAgent.enabled && enemyStates.enemyAgent.isOnNavMesh)
            enemyStates.enemyAgent.SetDestination(enemyStates.target.position);
        enemyStates.animator.SetBool("Move", true);
    }
    public override void Step()
    {
        base.Step();
        if (enemyStates.target != null && enemyStates.enemyAgent.enabled && enemyStates.enemyAgent.isOnNavMesh)
            enemyStates.enemyAgent.SetDestination(enemyStates.target.position);

        CheckSound();
        
        if (canPlayStepSound)
        {
            AudioClip clip = enemyStates.footSteps[Random.Range(0, enemyStates.footSteps.Length)];
            SoundManager.instance.PlaySound(clip, enemyStates.enemyController.transform.position, true);
            canPlayStepSound = false;
            time = clip.length;
            timePassed = 0;
        }
    }

    public override void Exit()
    {
        // Set move animation to false
        base.Exit();
        enemyStates.animator.SetBool("Move", false);
    }
    public bool TargetReached()
    {
        if (!enemyStates.enemyAgent.enabled || !enemyStates.enemyAgent.isOnNavMesh)
            return false;

        if (enemyStates.target == null)
            return false;

        if (enemyStates.enemyAgent.pathPending)
            return false;

        float dist = Vector3.Distance(
            enemyStates.enemyTransform.position,
            enemyStates.target.position
        );

        return dist <= enemyStates.enemyController.EnemyData.attackRange;
    }
    public bool TargetOutOfRange()
    {
        if (!enemyStates.enemyAgent.enabled || !enemyStates.enemyAgent.isOnNavMesh)
            return false;

        if (enemyStates.target == null)
            return true;

        if (enemyStates.enemyAgent.pathPending)
            return false;

        float dist = Vector3.Distance(enemyStates.enemyTransform.position, enemyStates.target.position);
        return dist > enemyStates.enemyController.EnemyData.detectionRange;
    }
    private void CheckSound()
    {
        if (canPlayStepSound) return;

        timePassed += Time.deltaTime;
        if (timePassed >= time)
            canPlayStepSound = true;
    }
}
