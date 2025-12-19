using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    ObjectPooling oPool;
    BaseWeaponStats bws;
    float timer;

    void Start()
    {
        oPool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ObjectPooling>();
        bws = gameObject.GetComponent<BaseWeaponStats>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bws.AttackRate.StatsValue())
        {
            timer = 0f;
            AimWeapon();
            
        }
    }

    private void AimWeapon()
    {
        GameObject tne = gameObject.GetComponentInParent<TrackNeareastEnemy>().NearestEnemy();
        Vector2 targetPos = tne.transform.position - transform.position;
        FireProjectiles(targetPos);
    }

    private void FireProjectiles(Vector2 targetPos)
    {
        float projectiles = bws.Projectiles.StatsValue();
        print("projectiles: " + bws.Projectiles.StatsValue());
        for (int i = 0; i < projectiles; i++)
        {
            GameObject projGO = oPool.objectPool[0];
            projGO.GetComponent<ProjectileBehaviour>().SetStats(bws.BaseDamage.StatsValue(), bws.WeapArea.StatsValue(), bws.LifeTime.StatsValue(), bws.ProjectileSpeed.StatsValue());
            projGO.SetActive(true);
            oPool.activePool.Add(projGO);
            oPool.objectPool.Remove(projGO);
            //projGO.GetComponent<ProjectileBehaviour>().SetStats(bws.BaseDamage.StatsValue(), bws.Area.StatsValue(), bws.LifeTime.StatsValue(), bws.ProjectileSpeed.StatsValue());
            projGO.transform.up = targetPos;
            projGO.transform.position = transform.position;
        }
    }   
}
