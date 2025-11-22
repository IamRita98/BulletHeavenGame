using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    ObjectPooling oPool;
    [SerializeField]float speed;
    float lifeTime;
    float timer = 0; 
    BaseWeaponStats bws;
    public static event System.Action<float, GameObject> OnAttackHit;

    private void Awake()
    {
        oPool = gameObject.transform.parent.GetComponent<ObjectPooling>();
        bws = gameObject.transform.parent.GetComponent<BaseWeaponStats>();
        lifeTime = bws.LifeTime.StatsValue();
    }
    

    private void Update()
    {
        
        if (!this.isActiveAndEnabled) return;
        if(timer >= lifeTime)
        {
            ReturnToPool();
            print("cum");
            timer = 0;
        }
        transform.Translate((Vector2.up * speed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        OnAttackHit?.Invoke(bws.BaseDamage.StatsValue(),collision.gameObject);
        /*
        EnemyBaseStats ebs = collision.GetComponent<EnemyBaseStats>();

        float dam=bws.BaseDamage.StatsValue(); 
        float health=ebs.Health.StatsValue();
        if (health-dam<= 0)
        {
            //enemy kill
        }
        else
        {
            ebs.Health.AddFlatValue((health - dam)*-1);
        }
        */
            ReturnToPool();
    }

    private void ReturnToPool()
    {
        oPool.objectPool.Add(gameObject);
        oPool.activePool.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
