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
    public Stats LifeTime;
    public Stats AttackRate;
    public Stats Area;
    public Stats ProjectileSpeed;
    public Stats Pierce;

    float attackTimer;

    private void Start()
    {
        
        MakeStats();   
    }

/*    private void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackRate)
        {
            Attack();
            attackTimer = 0;
        }
    }
*/
/*    private void Attack()
    {
        GameObject nearestEnemy = tNearestEnemy.NearestEnemy();
        for (int i = 0; i < Projectiles.StatsValue(); i++)
        {
            if (isMelee) MeleeAttack();
            else RangedAttack();
            
        }
    }*/

    private void MeleeAttack()
    {
        //Melee
    }

    private void RangedAttack()
    {
        //GameObject weaponGO = oPool.objectPool[0];
        //rotate bullet to face nearest enemy
        //t.transform.position = transform.position;
        //t.SetActive(true);
        //oPool.objectPool.Remove(t);
        //Spread projectiles based on number
        //t.transform.localScale *= bWeaponStats.ProjectileSize.StatsValue();
        //t.damage = damage; //Maybe pass each stat to bullet in this way
        //t.transform.looktowards == nearestenemy.transform.position
        //t.transform.translate(projectileSpeed)
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
