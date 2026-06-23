using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
/// <summary>
/// This class serves as a data container for the enemy's states in the finite state machine (FSM).
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(EnemyController))]
[DefaultExecutionOrder(100)]
public class StatesData : MonoBehaviour
{
    [Header("References")]
    public Transform enemyTransform;
    [ReadOnly] public Transform target;
    public NavMeshAgent enemyAgent;
    public Animator animator;
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
    public float stunDuration = 2f;
    public float throwDamage = 10f;

    private void Start()
    {
        enemyTransform = transform;

        target = FindAnyObjectByType<PlayerController>().transform;
        enemyAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        enemyAgent.stoppingDistance = enemyController.EnemyData.attackRange-(enemyController.EnemyData.attackRange*30/100);
        enemyAgent.speed = enemyController.EnemyData.moveSpeed;
        grabGnome = GetComponent<GrabGnome>();
        enemyController.health.damageEvent += Damaged;
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
