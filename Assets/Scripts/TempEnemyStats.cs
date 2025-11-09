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
        if(timer >= 3)
        {
            currentHP = 0;
            timer = 0;
        }
    }

    void Die()
    {
        currentHP = maxHP;
        oPool.objectPool.Add(gameObject);
        gameObject.SetActive(false);
    }
}
