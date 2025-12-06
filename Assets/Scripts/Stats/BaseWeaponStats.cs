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
    [SerializeField] float weapArea = 0;
    [SerializeField] float projectileSpeed;
    [SerializeField] float pierce = 0;
    
    public Stats BaseDamage;
    public Stats AttackRate;
    public Stats LifeTime;
    public Stats WeapArea;
    public Stats ProjectileSpeed;
    public Stats Pierce;
    
    float attackTimer;

    //upgrade tiers trackers
    public int bDamUpgT = 0;
    public int attackRUpgT = 0;
    public int weapAreaUpgT = 0;
    public int ProjSpeedUpgT =0;
    public int lifeTimeUpgT = 0;
    public int pierceUpgT = 0;


    private void Start()
    {
        MakeStats();   
    }

    private void MakeStats()
    {
        BaseDamage = new Stats(baseDamage);
        LifeTime = new Stats(lifeTime);
        AttackRate = new Stats(attackRate);
        WeapArea = new Stats(weapArea);
        ProjectileSpeed = new Stats(projectileSpeed);
        Pierce = new Stats(pierce);
    }
}
