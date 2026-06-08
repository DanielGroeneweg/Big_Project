using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
public class GrabGnome : MonoBehaviour
{
    [SerializeField] private Vector3 holdOffset = new Vector3(0, 0, 1.5f); // distance in front of camera

    private Rigidbody rb;
    private bool isGrabbed = false;

    private Collider col;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Grab(Transform cameraTransform)
    {
        isGrabbed = true;
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        col.enabled = false; // disable so raycast ignores it

        transform.SetParent(cameraTransform);
        transform.localPosition = holdOffset;
        transform.localRotation = Quaternion.identity;
    }

    public void Throw(Vector3 direction, float force)
    {
        isGrabbed = false;
        transform.SetParent(null);
        col.enabled = true; // re-enable on throw
        rb.isKinematic = false;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}