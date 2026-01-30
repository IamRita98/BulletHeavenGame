using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileBehaviour : MonoBehaviour
{
    Transform gunParent;
    ObjectPooling oPool;
    float speed;
    public float lifeTime;
    float damage;
    Vector3 baseArea;
    float area;
    public float timer = 0;
    CombatHandler combatHandler;
    int bulletPierce;
    public List<GameObject> listOfEnemiesHitByThisBullet = new List<GameObject>();
    [SerializeField] bool returnParent = false;

    private void Start()
    {
        oPool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ObjectPooling>();
        baseArea = transform.localScale;
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
        combatHandler.HandleDamage(damage, collision.gameObject);
        bulletPierce--;
        if(bulletPierce<0) ReturnToPool();
    }

    private void ReturnToPool()
    {
        listOfEnemiesHitByThisBullet = null;
        if (returnParent)
        {
            oPool.objectPool.Add(transform.parent.gameObject);
            oPool.activePool.Remove(transform.parent.gameObject);
        }
        else if (!returnParent)
        {
            oPool.objectPool.Add(gameObject);
            oPool.activePool.Remove(gameObject);
        }

        transform.localScale = baseArea;
        gameObject.SetActive(false);
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
}
