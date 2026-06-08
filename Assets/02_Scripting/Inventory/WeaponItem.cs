using UnityEngine;

[CreateAssetMenu(fileName = "WeaponItem", menuName = "Scriptable Objects/Weapon")]
public class WeaponItem : ScriptableObject
{
    [SerializeField] string itemName;
    [SerializeField] Sprite icon;
    [SerializeField] float damage;
    [SerializeField] float attackSpeed;
    [SerializeField] Weapon weaponPrefab;
    public string ItemName => itemName;
    public Sprite Icon => icon;
    public float Damage => damage;
    public float AttackSpeed => attackSpeed;
    public Weapon WeaponPrefab => weaponPrefab;
}
