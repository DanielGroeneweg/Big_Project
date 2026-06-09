using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// This class serves as a data container for the enemy's states in the finite state machine (FSM).
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(EnemyController))]
public class StatesData : MonoBehaviour
{
    [Header("References")]
    public Transform enemyTransform;
    public Transform target;
    public NavMeshAgent enemyAgent;
    public Animator animator;
    public Collider attackCollider;
    public EnemyController enemyController;
    public Animator weaponAnimator;
    public Weapon weapon;
    public Rigidbody rb;
    public GrabGnome grabGnome;

    [Header("Variables")]
    public bool isPickedUp = false;
    public bool isLanded = false;
    public bool isStunned = false;
    public float stunDuration = 2f;
    public bool wasThrown;


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

    }

}
