using UnityEngine;
public class Weapon : MonoBehaviour
{
    [SerializeField] Collider weaponCollider;
    float damage;
    public void Attack(float attackDuration, float damage)
    {
        this.damage = damage;

        weaponCollider.enabled = true;

        Invoke(nameof(DisableAttack), attackDuration);
    }
    void DisableAttack()
    {
        weaponCollider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon had collision!");
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            Debug.Log("found health component!");
            health.Damage(damage);
            weaponCollider.enabled = false;
        }
    }
    private void Start()
    {
        weaponCollider.enabled = false;
    }
}