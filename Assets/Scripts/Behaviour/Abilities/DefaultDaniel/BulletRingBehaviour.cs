using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRingBehaviour : MonoBehaviour
{
    ObjectPooling oPool;
    GameObject parentGO;
    AbilityStats abilityStats;
    bool foundNearestEnemy;
    CombatHandler combatHandler;
    BaseWeaponStats baseWeaponStats;
    float spread;
    int baseBulletAmount = 7;
    public GameObject projectile;
    float timer;
    float lifeTime = .2f;

    private void Awake()
    {
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        abilityStats = GetComponent<AbilityStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Weapon").GetComponent<BaseWeaponStats>();
        oPool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ObjectPooling>();
    }

    private void OnEnable()
    {
        SpawnRing();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime) gameObject.SetActive(false);
    }

    private void SpawnRing()
    {
        float projectiles = baseBulletAmount + baseWeaponStats.Projectiles.StatsValue();
        spread = 360 / projectiles;//360=circle
        for (int i = 0; i < projectiles; i++)
        {
            GameObject projectile = oPool.objectPool[0];
            projectile.GetComponent<ProjectileBehaviour>().SetStats(baseWeaponStats.BaseDamage.StatsValue(), baseWeaponStats.WeapArea.StatsValue(), baseWeaponStats.LifeTime.StatsValue(), baseWeaponStats.ProjectileSpeed.StatsValue(), baseWeaponStats.Pierce.StatsValue());
            projectile.SetActive(true);
            oPool.activePool.Add(projectile);
            oPool.objectPool.Remove(projectile);
            projectile.transform.position = gameObject.transform.position;
            Vector3 spreadPos=new Vector3(0, 0, spread*i);
            projectile.transform.eulerAngles= spreadPos;
        }
    }
}
