using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
public class WeaponDropManager : MonoBehaviour
{
    [Tooltip("The amount of enemies needed to be killed without dropping a weapon before guarenteeing a drop")]
    [SerializeField] int guarenteedDropAttempt;
    [SerializeField][Range(0f, 1f)] float dropChance;
    [SerializeField] WeaponDrop weaponDropPrefab;
    int enemiesKilledSinceDrop = 0;
    private IEnumerator Start()
    {
        // make the first enemy drop a weapon 100% of the time
        enemiesKilledSinceDrop = guarenteedDropAttempt;
        yield return new WaitForEndOfFrame();
        EventBusManager.instance.DropWeaponEvent.Register(WeaponDrop);
    }
    void WeaponDrop(DropWeaponEventData data)
    {
        enemiesKilledSinceDrop++;
        float rng = Random.Range(0f, 1f);
        Debug.Log($"{enemiesKilledSinceDrop} enemies killed since last drop {enemiesKilledSinceDrop} >= {guarenteedDropAttempt} = {enemiesKilledSinceDrop >= guarenteedDropAttempt}" +
            $"\n{rng} <= {dropChance} = {rng <= dropChance}" +
            "\n Dropping a weapon if one or both is true!");
        if (enemiesKilledSinceDrop >= guarenteedDropAttempt || rng <= dropChance)
        {
            WeaponDrop drop = Instantiate(weaponDropPrefab, data.position, Quaternion.identity);
            drop.SpawnWeapon(data.weapon);
            enemiesKilledSinceDrop = 0;
        }
    }
}