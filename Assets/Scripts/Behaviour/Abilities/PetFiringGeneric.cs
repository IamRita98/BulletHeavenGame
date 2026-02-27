using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetFiringGeneric : MonoBehaviour
{
    ObjectPooling oPool;
    AbilityStats abilityStats;
    float timer;
    public float totalSpread = 45;
    public bool activate = false;
    public float abilityAttackRate = .25f;
    bool inCombat = false;
    public bool isUpgraded = false;
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        if (SceneManager.GetActiveScene().name != "MainMenu") GetReferences(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
        if (isUpgraded)
        {
            activate = true;
        }
        else
        {
            return;
        }
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
    }

    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        oPool = GameObject.FindGameObjectWithTag("PetProjPool").GetComponent<ObjectPooling>();
        abilityStats = gameObject.GetComponent<AbilityStats>();
    }
    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= abilityAttackRate)
        {
            timer = 0;
            AimWeapon();
        }
    }
    private void AimWeapon()
    {
        GameObject tne = GameObject.FindGameObjectWithTag("Player").GetComponent<TrackNeareastEnemy>().NearestEnemy();
        Vector2 targetPos = tne.transform.position - transform.position;
        FireProjectiles(targetPos);
    }
    private void FireProjectiles(Vector2 targetPos)
    {
        float projectiles = abilityStats.Projectiles.StatsValue() + 1;
        //float spread = totalSpread / projectiles;
        if (activate)
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
                SpriteRenderer bulletRenderer;
                bulletRenderer = spawnedBullet.GetComponent<SpriteRenderer>();
                bulletRenderer.color = Color.red;
                GiveBulletDirection(spawnedBullet, targetPos, spreadPosition);
            }
        }


    }
    GameObject SpawnBullet()
    {
        GameObject projGO = oPool.objectPool[0];
        projGO.GetComponent<ProjectileBehaviour>().SetStats(abilityStats.BaseDamage.StatsValue(), abilityStats.Area.StatsValue(), abilityStats.LifeTime.StatsValue(), abilityStats.ProjectileSpeed.StatsValue() + 9f, abilityStats.Pierce.StatsValue());
        print("Spawned ability proj: ");
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
