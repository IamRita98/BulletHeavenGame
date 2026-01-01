using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    ObjectPooling oPool;
    BaseWeaponStats bws;
    float timer;
    public float totalSpread = 45;
    public bool isSpread;

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
        //float spread = totalSpread / projectiles;
        if (isSpread)
        {
            for (int i = 0; i < projectiles; i++)//for spread
            {
                Vector3 spreadPosition = Vector3.zero;
                if (i != 0)
                {
                    float randomSpread = Random.Range(-totalSpread, totalSpread);
                    spreadPosition = new Vector3(0, 0, randomSpread);
                }

                GameObject spawnedBullet = SpawnBullet();
                GiveBulletDirection(spawnedBullet, targetPos, spreadPosition);
            }
        }
        else
        {//burst mode

        }


    }

    GameObject SpawnBullet()
    {
        GameObject projGO = oPool.objectPool[0];
        projGO.GetComponent<ProjectileBehaviour>().SetStats(bws.BaseDamage.StatsValue(), bws.WeapArea.StatsValue(), bws.LifeTime.StatsValue(), bws.ProjectileSpeed.StatsValue(), bws.Pierce.StatsValue());
        print("bullet dam: " + bws.BaseDamage.StatsValue());
        projGO.SetActive(true);
        oPool.activePool.Add(projGO);
        oPool.objectPool.Remove(projGO);
        return projGO;
    }
    
    void GiveBulletDirection(GameObject spawnedBullet, Vector2 targetPos, Vector3 spreadPosition)
    {
        spawnedBullet.transform.up = targetPos;
        spawnedBullet.transform.position = transform.position;
        spawnedBullet.transform.eulerAngles += spreadPosition;
    }
}
