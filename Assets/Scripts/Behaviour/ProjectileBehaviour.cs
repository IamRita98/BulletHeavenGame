using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileBehaviour : MonoBehaviour
{
    ObjectPooling oPool;
    float speed;
    public float lifeTime;
    float damage;
    Vector3 baseArea;
    float area;
    public float timer = 0;
    CombatHandler combatHandler;
    public int bulletPierce;
    public List<GameObject> listOfEnemiesHitByThisBullet = new List<GameObject>();
    [SerializeField] bool returnParent = false;
    public bool shouldChain = false;
    TrackNeareastEnemy nearestEnemy;
    public float timesToChain = 0;
    float spread = 20;
    int chainCounter = 0;
    public bool shouldSplit=false;
    public bool durationT3 = false;

    private void Start()
    {
        oPool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ObjectPooling>();
        baseArea = transform.localScale;
        nearestEnemy = gameObject.GetComponent<TrackNeareastEnemy>();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        if (SceneManager.GetActiveScene().name != "MainMenu") GetReferences(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
    }

    void GetReferences(Scene oldScene, Scene newScene)
    {
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ReturnToPool();
            timer = 0;
        }
        transform.Translate((Vector2.up * speed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        foreach(GameObject enemy in listOfEnemiesHitByThisBullet)
        {
            if (collision.gameObject == enemy)
            {
                print("Passed");
                return;
            }
        }
        listOfEnemiesHitByThisBullet.Add(collision.gameObject);
        combatHandler.HandleDamage(damage, collision.gameObject,CombatHandler.DamageType.Physical);
        if (shouldChain && timesToChain > 0)
        {
            if (shouldSplit && chainCounter >= 1) SpawnSplitProjectile();
            ChainTowardsNextTarget();
            
            chainCounter++;
            timesToChain--;
        }
        else if (shouldChain && timesToChain == 0) ReturnToPool();
        else bulletPierce--;
            
        if(bulletPierce < 0) ReturnToPool();
    }

    private void ReturnToPool()
    {
        listOfEnemiesHitByThisBullet.Clear();
        if (returnParent)
        {
            oPool.objectPool.Add(transform.parent.gameObject);
            oPool.activePool.Remove(transform.parent.gameObject);
            transform.localScale = baseArea;
            timesToChain = 0;
            shouldChain = false;
            transform.parent.gameObject.SetActive(false);
        }
        else if (!returnParent)
        {
            oPool.objectPool.Add(gameObject);
            oPool.activePool.Remove(gameObject);
            transform.localScale = baseArea;
            timesToChain = 0;
            shouldChain = false;
            gameObject.SetActive(false);
        }
        
    }

    public void SetStats(float dam, float size, float lTime, float moveSpeed, float pierce)
    {
        bulletPierce = ((int)pierce);
        damage = dam;
        lifeTime = lTime;
        area = size;
        speed = moveSpeed;
        transform.localScale *= area;
    }

    void SpawnSplitProjectile()
    {
        for (int i = 0;i < 2; i ++){
            GameObject projectile = oPool.objectPool[0];
            ProjectileBehaviour pBehaviour = projectile.GetComponent<ProjectileBehaviour>();
            BaseWeaponStats baseWeaponStats = GameObject.FindGameObjectWithTag("Weapon").GetComponent<BaseWeaponStats>();
            pBehaviour.SetStats(baseWeaponStats.BaseDamage.StatsValue(), baseWeaponStats.WeapArea.StatsValue(), baseWeaponStats.LifeTime.StatsValue()/4, baseWeaponStats.ProjectileSpeed.StatsValue()/4, baseWeaponStats.Pierce.StatsValue());

            projectile.SetActive(true);
            oPool.activePool.Add(projectile);
            oPool.objectPool.Remove(projectile);
            projectile.transform.position = gameObject.transform.position;
            projectile.transform.up = this.transform.up;
            Vector3 spreadPosition;
            if (i != 0)
            {
                spreadPosition = new Vector3(0, 0, -spread);

            }
            else
            {
                spreadPosition = new Vector3(0, 0, spread);//spread*i+desired spread
            }
                
            projectile.transform.eulerAngles += spreadPosition;
        }
        
    }
    void ChainTowardsNextTarget()
    {
        GameObject nextNearestEnemy = nearestEnemy.NearestEnemy(listOfEnemiesHitByThisBullet);
        Vector2 targetPos = nextNearestEnemy.transform.position - transform.position;
        transform.up = targetPos;
    }
}
