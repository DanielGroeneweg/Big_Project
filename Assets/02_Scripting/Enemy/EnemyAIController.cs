using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    private FSM fsm;
    private StatesData data;

    [SerializeField] private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        fsm = new GnomeFSM(data);
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Step();
    }
}
