using UnityEngine;
using UnityEngine.AI;

public class StatesData : MonoBehaviour
{
    [Header("References")]
    public Transform enemyTransform;
    public Transform target;
    public NavMeshAgent enemyAgent;
    public Animator animator;
    public Collider attackCollider;
    public EnemyController enemyController;

    private void Start()
    {
        enemyTransform = transform;
        // target = FindObjectOfType<PlayerController>().transform;
        //enemyAgent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        enemyAgent.stoppingDistance = enemyController.EnemyData.attackRange-(enemyController.EnemyData.attackRange*30/100);
    }

}
