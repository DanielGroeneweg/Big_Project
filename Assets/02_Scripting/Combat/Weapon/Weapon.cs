using UnityEngine;
public class Weapon : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    float damage;
    public void Attack(float attackDuration, float damage)
    {
        this.damage = damage;

        foreach (Collider collider in colliders)
            collider.enabled = true;

        Invoke(nameof(DisableAttack), attackDuration);
    }
    void DisableAttack()
    {
        foreach (Collider collider in colliders)
            collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon had collision!");
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            Debug.Log("found health component!");
            health.Damage(damage);
        }
    }
    private void Start()
    {
        foreach (Collider collider in colliders)
            collider.enabled = false;
    }
}