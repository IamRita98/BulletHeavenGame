using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    Transform gunParent;
    ObjectPooling oPool;
    float speed;
    float lifeTime;
    float damage;
    Vector3 baseArea;
    float area;
    float timer = 0;
    //BaseWeaponStats bws;
    public static event System.Action<float, GameObject> OnAttackHit;

    private void Start()
    {
        oPool = gameObject.GetComponentInParent<ObjectPooling>();
        //bws = GameObject.FindGameObjectWithTag("Weapon").GetComponent<BaseWeaponStats>();
        baseArea = transform.localScale;
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
        OnAttackHit?.Invoke(damage, collision.gameObject);
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        oPool.objectPool.Add(gameObject);
        oPool.activePool.Remove(gameObject);
        transform.localScale = baseArea;
        gameObject.SetActive(false);
    }

    public void SetStats(float dam, float size, float lTime, float moveSpeed)
    {
        damage = dam;
        lifeTime = lTime;
        area = size;
        speed = moveSpeed;
        transform.localScale *= area;
    }
}
