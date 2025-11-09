using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemyPrefab;
    Camera camera;
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
        camera = Camera.main.GetComponent<Camera>(); //(0,0)bot left and (1,1) top right
        print(oPool);
        SpawnFunc();
    }

    // Update is called once per frame
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
        int enemiesPerWave = enemiesToSpawn+scalingDiff;
        for (int i = 0; i < enemiesToSpawn+scalingDiff; i++)
        {
            int r1 = Random.Range(1, 5);
            if (r1 == 1)
            {
                //top spawn
                spawnPos = camera.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), 1 + spawnBuffer));

            }
            if (r1 == 2)
            {
                //right spawn
                spawnPos = camera.ViewportToWorldPoint(new Vector2(1 + spawnBuffer, Random.Range(0f, 1f)));

            }
            if (r1 == 3)
            {
                //bottom spawn
                spawnPos = camera.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), 0 - spawnBuffer));

            }
            if (r1 == 4)
            {
                //left spawn
                spawnPos = camera.ViewportToWorldPoint(new Vector2(0 - spawnBuffer, Random.Range(0f, 1f)));

            }


            GameObject t = oPool.objectPool[0];
            t.transform.position = spawnPos;
            t.SetActive(true);
            oPool.objectPool.Remove(t);
            enemiesPerWave--;

        }
        scalingDiff++;
    }
}
