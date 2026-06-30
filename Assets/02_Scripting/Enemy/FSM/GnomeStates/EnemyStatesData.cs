using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyController))]
[DefaultExecutionOrder(100)]
public class EnemyStatesData : StatesData
{
    [Header("References")]
    public Transform enemyTransform;
    [ReadOnly] public Transform target;
    public NavMeshAgent enemyAgent;
    public Collider attackCollider;
    public EnemyController enemyController;
    public Weapon weapon;
    public Rigidbody rb;
    public GrabGnome grabGnome;
    public List<Image> enemyHealthBar;

    [Header("Sounds")]
    public AudioClip[] footSteps;

    [Header("Variables")]
    public bool isPickedUp = false;
    public bool isLanded = false;
    public bool isStunned = false;
    public bool isDamaged = false;
    public bool wasThrown;
    public bool ignoreDamageState;
    public float stunDuration = 2f;
    public float throwDamage = 10f;
    public string attackClipName = "Fighting_animation_01";
    [ReadOnly] public float attackAnimatorSpeed = 1f;
    [HideInInspector] public Vector3 colliderLocalPosition;
    [HideInInspector] public Quaternion colliderLocalRotation;

    private void Start()
    {
        enemyTransform = transform;

        target = FindAnyObjectByType<PlayerController>().transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        enemyController = GetComponent<EnemyController>();
        enemyAgent.stoppingDistance = enemyController.EnemyData.attackRange - (enemyController.EnemyData.attackRange * 30 / 100);
        enemyAgent.speed = enemyController.EnemyData.moveSpeed;
        grabGnome = GetComponent<GrabGnome>();
        enemyController.health.damageEvent += Damaged;
        colliderLocalPosition = attackCollider.transform.localPosition;
        colliderLocalRotation = attackCollider.transform.localRotation;
        CalculateAttackAnimatorSpeed();
    }

    private void CalculateAttackAnimatorSpeed()
    {
        if (animator == null || animator.runtimeAnimatorController == null)
        {
            attackAnimatorSpeed = 1f;
            return;
        }

        AnimationClip attackClip = System.Array.Find(
            animator.runtimeAnimatorController.animationClips,
            clip => clip.name == attackClipName);

        float targetDuration = enemyController.EnemyData.attackDuration;

        if (attackClip == null || targetDuration <= 0f)
        {
            Debug.LogWarning($"StatesData: could not find clip '{attackClipName}' or invalid attackCountdown, defaulting attackAnimatorSpeed to 1.");
            attackAnimatorSpeed = 1f;
            return;
        }

        attackAnimatorSpeed = attackClip.length / targetDuration;
    }

    public void SetKinematic(bool kinematic)
    {
        rb.isKinematic = kinematic;
        SetAgentStopped(kinematic);
    }
    public void SetAgentStopped(bool stopped)
    {
        if (enemyAgent.enabled && enemyAgent.isOnNavMesh)
            enemyAgent.isStopped = stopped;
    }
    public void Damaged()
    {
        isDamaged = true;
    }
}
