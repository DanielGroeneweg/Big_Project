using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class Weapon : MonoBehaviour
{
    [Tooltip("The percentage of the attack duration at which the collider becomes enabled to prevent the feeling of being hit by something too early")]
    [SerializeField][Range(0f, 1f)] float colliderEnableDelay = 0.4f;
    [SerializeField] Collider weaponCollider;
    [Tooltip("Area of effect")]
    [SerializeField] bool isAOE;
    [SerializeField] bool hasDurability;
    [SerializeField] AudioClip[] weaponSounds;
    [HideInInspector] public float durability;
    [HideInInspector] public float maxDurability;
    float damage;
    List<Health> hitObjects = new();
    public void Attack(float attackDuration, float damage)
    {
        if (weaponSounds.Length > 0)
        {
            int rng = Random.Range(0, weaponSounds.Length);
            AudioClip clip = weaponSounds[rng];
            SoundManager.instance.PlaySound(clip, transform.position, true);
        }

        this.damage = damage;

        Invoke(nameof(EnableAttack), attackDuration * colliderEnableDelay);
        Invoke(nameof(DisableAttack), attackDuration);
    }
    void EnableAttack()
    {
        weaponCollider.enabled = true;
    }
    void DisableAttack()
    {
        weaponCollider.enabled = false;
    }
    private void Start()
    {
        weaponCollider.enabled = false;

        if (gameObject.layer == LayerMask.NameToLayer("EnemyWeapon")) return;

        WeaponDurabilityEventData eventData = null;
        
        if (!hasDurability)
            eventData = new WeaponDurabilityEventData() { durability = maxDurability, maxDurability = maxDurability };

        else
            eventData = new WeaponDurabilityEventData() { durability = durability, maxDurability = maxDurability };
        
        EventBusManager.instance.WeaponDurabilityEvent.Raise(eventData);
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Weapon had collision!");
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            hitObjects.Add(health);
            StartCoroutine(HandleList());
        }
    }
    IEnumerator HandleList()
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

        if (hasDurability)
        {
            durability--;
            if (durability <= 0)
            {
                EquipWeaponEventData eventData = new EquipWeaponEventData() { weapon = null, oldWeaponDestroyed = true };
                EventBusManager.instance.EquipWeaponEvent.Raise(eventData);
            }

            else
            {
                WeaponDurabilityEventData eventData = new WeaponDurabilityEventData() { durability = durability, maxDurability = maxDurability };
                EventBusManager.instance.WeaponDurabilityEvent.Raise(eventData);
            }
        }

        StopAllCoroutines();
    }
}