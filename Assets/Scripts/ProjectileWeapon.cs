using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    ObjectPooling oPool;
    [SerializeField] string weaponName;

    BaseWeaponStats bWeaponStats;
    BaseStats bStats;
    TrackNeareastEnemy tNear;
    float fireRate;
    float damage;
    float lifeTime;
    float size;
    public Stats Pierce;
    [SerializeField] float pierce;
    public Stats ProjectileSpeed;
    [SerializeField] float projectilesSpeed;

    float timer;

    private void Start()
    {
        bStats = gameObject.transform.parent.GetComponent<BaseStats>();
        bWeaponStats = gameObject.GetComponent<BaseWeaponStats>();
        oPool = gameObject.GetComponent<ObjectPooling>();
        tNear = gameObject.transform.parent.GetComponent<TrackNeareastEnemy>();
        MakeStats();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > fireRate)
        {
            FireBullet();
            timer = 0;
        }
    }

    private void MakeStats()
    {
        fireRate = bWeaponStats.FireRate.StatsValue();
        damage = bWeaponStats.BaseDamage.StatsValue();
        lifeTime = bWeaponStats.LifeTime.StatsValue();
        size = bWeaponStats.Area.StatsValue();
        Pierce = new Stats(pierce);
        ProjectileSpeed = new Stats(projectilesSpeed);
    }

    private void FireBullet()
    {
        GameObject nearestEnemy = tNear.NearestEnemy();
        for (int i = 0; i < bStats.Projectiles.StatsValue(); i++)
        {   //rotate bullet to face nearest enemy
            //GameObject t = oPool.objectPool[0];
            //t.transform.position = transform.position;
            //t.SetActive(true);
            //oPool.objectPool.Remove(t);
            //Spread projectiles based on number
            //t.transform.localScale *= bWeaponStats.ProjectileSize.StatsValue();
            //t.damage = damage; //Maybe pass each stat to bullet in this way
            //t.transform.looktowards == nearestenemy.transform.position
            //t.transform.translate(projectileSpeed)

        }
    }
}
