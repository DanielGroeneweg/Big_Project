using UnityEngine;
using UnityEngine.AI;
using System.Collections;
/// <summary>
/// This class is responsible for controlling the enemy's AI behavior using a finite state machine (FSM). 
/// It requires the NavMeshAgent, EnemyController, and StatesData components to function properly. 
/// The FSM is set up in the Start method. 
/// The Update method calls the Step function of the FSM to update the enemy's behavior every frame.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(StatesData))]
[DefaultExecutionOrder(100)]
public class EnemyAIController : MonoBehaviour
{
    private FSM fsm;
    private StatesData data;

    [SerializeField] private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        target = PlayerController.instance.transform;

        FSMSetUp();
        fsm.Enter();
    }

    private void FSMSetUp()
    {
        data = GetComponent<StatesData>();
        data.enemyTransform = transform;
        data.enemyAgent = GetComponent<NavMeshAgent>();
        data.enemyController = GetComponent<EnemyController>();
        data.target = target;
        data.rb = GetComponent<Rigidbody>();
        fsm = new GnomeFSM(data);
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Step();
    }
}
