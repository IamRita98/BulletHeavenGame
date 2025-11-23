using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeaponStats : BaseStats
{
    TrackNeareastEnemy tNearestEnemy;
    [SerializeField] bool isMelee;
    [SerializeField] float baseDamage = 0;
    [SerializeField] float attackRate = 0;
    [SerializeField] float lifeTime = 0;
    [SerializeField] float area = 0;
    [SerializeField] float projectileSpeed;
    [SerializeField] float pierce = 0;
    
    public Stats BaseDamage;
    public Stats AttackRate;
    public Stats LifeTime;
    public Stats Area;
    public Stats ProjectileSpeed;
    public Stats Pierce;

    float attackTimer;

    private void Start()
    {
        MakeStats();   
    }

    private void MakeStats()
    {
        BaseDamage = new Stats(baseDamage);
        LifeTime = new Stats(lifeTime);
        AttackRate = new Stats(attackRate);
        Area = new Stats(area);
        ProjectileSpeed = new Stats(projectileSpeed);
        Pierce = new Stats(pierce);
    }
}
