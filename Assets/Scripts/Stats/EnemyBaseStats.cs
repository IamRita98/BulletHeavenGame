using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseStats : BaseStats
{
    [SerializeField] float contactDamage;
    [SerializeField] float attackDamage;

    public Stats ContactDamage;
    public Stats AttackDamage;

    ObjectPooling oPool;
    private void Start()
    {
        
        MakeStats();
        oPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPooling>();
    }

    void Update()
    {
        
    }
    public void ReturnToPool()
    {
        oPool.objectPool.Add(gameObject);
        oPool.activePool.Remove(gameObject);
        gameObject.SetActive(false);
    }

    private void MakeStats()
    {
        ContactDamage = new Stats(contactDamage);
        AttackDamage = new Stats(attackDamage);
    }
}
