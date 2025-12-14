using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    float spawnBuffer = 0.2f;
    Vector2 spawnPos;
    ObjectPooling oPool;
    [SerializeField] int enemiesToSpawn = 10;
    float timer = 0;
    int scalingDiff = 0;
    float timeToNextWave = 5;

    void Start()
    {
        oPool = gameObject.GetComponentInParent<ObjectPooling>();
        
        SpawnFunc();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToNextWave)
        {
            SpawnFunc();
            timer = 0;
        }
    }

    void SpawnFunc()
    {
        int enemiesPerWave = enemiesToSpawn + scalingDiff;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            float x = Random.Range(0f, 1f);
            float y = Random.Range(0f, 1f);

            int side = Random.Range(0, 4); // 0 - 3 represents the dimensional shifts

            switch (side)
            {
                case 0: y = 1f + spawnBuffer; break;
                case 1: x = 1f + spawnBuffer; break;
                case 2: y = 0f - spawnBuffer; break;
                case 3: x = 0f - spawnBuffer; break;
            }
            spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(x, y)); //(0,0)bot left and (1,1) top right

            GameObject enemySpawned = oPool.objectPool[0];
            EnemyBaseStats eBaseStats = enemySpawned.GetComponent<EnemyBaseStats>();
            eBaseStats.Health.StatOverwrite(eBaseStats.MaxHealth.StatsValue());
            enemySpawned.transform.position = spawnPos;
            enemySpawned.SetActive(true);
            oPool.objectPool.Remove(enemySpawned);
            oPool.activePool.Add(enemySpawned);
        }
        scalingDiff++;
    }
}
