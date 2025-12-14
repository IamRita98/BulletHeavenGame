using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRingBehaviour : MonoBehaviour
{
    ObjectPooling oPool;
    GameObject parentGO;
    AbilityStats abilityStats;
    bool foundNearestEnemy;
    float timer;
    CombatHandler combatHandler;
    BaseWeaponStats baseWeaponStats;
    float spread;
    int baseBulletAmount = 7;

    private void Awake()
    {
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        abilityStats = GetComponent<AbilityStats>();
        
        baseWeaponStats = GameObject.FindGameObjectWithTag("Weapon").GetComponent<BaseWeaponStats>();
    }
    private void OnEnable()
    {
        oPool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ObjectPooling>();
        timer = baseWeaponStats.LifeTime.StatsValue();
        SpawnRing();
    }
    private void Start()
    {
        
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= abilityStats.Cooldown.StatsValue())
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }
    private void SpawnRing()
    {
        
        float projectiles = baseBulletAmount + baseWeaponStats.Projectiles.StatsValue();
        spread = 360 / projectiles;//360=circle
        for (int i = 0; i < projectiles; i++)
        {
            GameObject projectile = oPool.objectPool[0];
            projectile.GetComponent<ProjectileBehaviour>().SetStats(baseWeaponStats.BaseDamage.StatsValue(), baseWeaponStats.WeapArea.StatsValue(), baseWeaponStats.LifeTime.StatsValue(), baseWeaponStats.ProjectileSpeed.StatsValue());
            projectile.SetActive(true);
            oPool.activePool.Add(projectile);
            oPool.objectPool.Remove(projectile);
            projectile.transform.position = gameObject.transform.position;
            Vector3 spreadPos=new Vector3(0, 0, spread*i);
            projectile.transform.eulerAngles= spreadPos;
        }
    }
}
