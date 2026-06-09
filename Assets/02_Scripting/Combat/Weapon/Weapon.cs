using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class Weapon : MonoBehaviour
{
    [SerializeField] protected Collider weaponCollider;
    [SerializeField] bool isAOE;
    protected float damage;
    protected List<Health> hitObjects = new();
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
    private void Start()
    {
        weaponCollider.enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon had collision!");
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            Debug.Log("adding");
            hitObjects.Add(health);
            StartCoroutine(HandleList());
        }
    }
    protected virtual IEnumerator HandleList()
    {
        yield return new WaitForEndOfFrame();

        // handle collision here
        if (isAOE)
        {
            foreach (Health health in hitObjects)
                health.Damage(damage);

            DisableAttack();
        }
        else
        {
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
        }
            

        hitObjects.Clear();

        StopAllCoroutines();
    }
}