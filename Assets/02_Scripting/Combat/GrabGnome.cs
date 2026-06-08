using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class GrabGnome : MonoBehaviour
{
    [SerializeField] private Vector3 holdOffset = new Vector3(0, 0, 1.5f);

    private Rigidbody rb;
    private StatesData data;

    private Collider col;

    [SerializeField] float groundedRange;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        data = GetComponent<StatesData>();
    }

    public void Grab(Transform cameraTransform)
    {
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        col.enabled = false;

        data.isPickedUp = true;

        transform.SetParent(cameraTransform);
        transform.localPosition = holdOffset;
        transform.localRotation = Quaternion.identity;
    }

    public void Throw(Vector3 direction, float force)
    {
        transform.SetParent(null);
        col.enabled = true; 
        rb.isKinematic = false;
        rb.AddForce(direction * force, ForceMode.Impulse);
        data.isLanded = false;
        data.isPickedUp = false;
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
            data.enemyAgent.enabled = true;
        }
    }
}