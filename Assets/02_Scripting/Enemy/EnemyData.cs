using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int maxHP;
    public float moveSpeed;
    public int attackDamage;
    public float attackRange;
    public float attackDuration;
    public float attackCooldown;
    public float detectionRange;
    public float rotateSpeed;
    public WeaponItem weapon;
   

    public Enemy CreateEnemy()
    {
        return new Enemy(maxHP, moveSpeed, attackDamage, attackRange, attackDuration, attackCooldown,detectionRange, rotateSpeed);
    }
}


[Serializable]
public class Enemy
{
    public float MaxHP => maxHP;
    private float maxHP;
    public float currentHP;
    public float MoveSpeed => moveSpeed;
    private float moveSpeed;

    public int AttackDamage => attackDamage;
    private int attackDamage;
    public float AttackRange => attackRange;
    private float attackRange;
    public float AttackDuration => attackDuration;
    private float attackDuration;
    private float AttackCooldown => attackCooldown;
    public float attackCooldown;
    public float DetectionRange => detectionRange;
    private float detectionRange;
    public float RotateSpeed => rotateSpeed;
    private float rotateSpeed;

    public Enemy(int maxHP, float moveSpeed, int attackDamage, float attackRange, float attackDuration,float attackCooldown, float detectionRange, float rotateSpeed)
    {
        this.maxHP = maxHP;
        currentHP = maxHP;
        this.moveSpeed = moveSpeed;
        this.attackDamage = attackDamage;
        this.attackRange = attackRange;
        this.attackDuration = attackDuration; 
        this.attackCooldown = attackCooldown;
        this.detectionRange = detectionRange;
        this.rotateSpeed = rotateSpeed;
    }
}