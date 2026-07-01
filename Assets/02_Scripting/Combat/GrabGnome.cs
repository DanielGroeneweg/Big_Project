using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class GrabGnome : MonoBehaviour
{
    [SerializeField] private Vector3 holdOffset = new Vector3(0, 0, 1.5f);
    [SerializeField] private Vector3 scale = new Vector3(75, 75, 75);

    private Vector3 initialScale;

    private Rigidbody rb;
    private EnemyStatesData data;

    private Collider col;

    [SerializeField] float groundedRange;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        data = GetComponent<EnemyStatesData>();
    }

    public void Grab(Transform parentTransform)
    {
        data.enemyAgent.enabled = false;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        col.enabled = false;
        initialScale = transform.localScale;
        data.isPickedUp = true;

        transform.SetParent(parentTransform);
        transform.localPosition = holdOffset;
        transform.localScale = scale;
        
        //transform.localRotation = new Quaternion();
    }

    public void Throw(Vector3 direction, float force)
    {
        transform.SetParent(null);
        transform.localScale = initialScale;
        col.enabled = true; 
        rb.isKinematic = false;
        rb.AddForce(direction * force, ForceMode.Impulse);
        data.isLanded = false;
        data.isPickedUp = false;
        data.wasThrown = true;
    }
    public bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, groundedRange))
        {
            if (hit.collider.tag == "ground") return true;
        }
        return false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!data.isPickedUp && collision.gameObject.CompareTag("ground"))
        {
            data.isLanded = true;
            data.wasThrown = false; 
        }
    }
}