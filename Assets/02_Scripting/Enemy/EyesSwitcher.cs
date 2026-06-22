using Unity.VisualScripting;
using UnityEngine;
[DefaultExecutionOrder(150)]
public class EyesSwitcher : MonoBehaviour
{
    [SerializeField] Material glowingMaterial;
    [SerializeField] Material notGlowingMaterial;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] float detectionAngle;
    Transform target;
    private void Start()
    {
        target = PlayerController.instance.transform;
    }
    private void Update()
    {
        float angle = Vector3.Dot(transform.forward, (target.position - transform.position).normalized);
        if (angle >= detectionAngle) mesh.material = glowingMaterial;
        else mesh.material = notGlowingMaterial;
    }
}