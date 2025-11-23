using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    ObjectPooling oPool;
    BaseWeaponStats bws;
    float timer;
    Vector2 dir;
    void Start()
    {
        oPool = gameObject.GetComponent<ObjectPooling>();
        bws = gameObject.GetComponent<BaseWeaponStats>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bws.AttackRate.StatsValue())
        {
            AimWeapon();
            timer = 0f;
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
        //if (dir == null) return;
        float projectiles = bws.Projectiles.StatsValue();
        print(projectiles);
        for (int i = 0; i < projectiles; i++)
        {
            GameObject projGO = oPool.objectPool[0];
            projGO.SetActive(true);
            oPool.activePool.Add(projGO);
            oPool.objectPool.Remove(projGO);
            projGO.transform.up = targetPos;
            projGO.transform.position = transform.position;
            print(projGO);
        }
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
}
