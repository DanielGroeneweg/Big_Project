using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class Weapon : MonoBehaviour
{
    [SerializeField] Collider weaponCollider;
    float damage;
    List<Health> hitObjects = new();
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
    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon had collision!");
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            hitObjects.Add(health);
            StartCoroutine(ClearList());
        }
    }
    private void Start()
    {
        weaponCollider.enabled = false;
    }
    protected virtual IEnumerator ClearList()
    {
        yield return new WaitForEndOfFrame();

        // handle collision here
        Health closest = null;
        float closestDist = Mathf.Infinity;
        foreach (Health health in hitObjects)
        {
            float dist = Vector3.Distance(transform.position, health.transform.position);
            if (dist < closestDist)
            {
                closest = health;
                closestDist = dist;
            }
        }

        if (closest != null)
        {
            closest.Damage(damage);
            DisableAttack();
        }

        hitObjects.Clear();

        StopAllCoroutines();
    }
}