using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyStats : MonoBehaviour
{
    public int maxHP = 1;
    public int currentHP;

    float timer;

    ObjectPooling oPool;

    private void Start()
    {
        currentHP = maxHP;
        oPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPooling>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        currentHP = maxHP;
        oPool.objectPool.Add(gameObject);
        gameObject.SetActive(false);
    }
}
