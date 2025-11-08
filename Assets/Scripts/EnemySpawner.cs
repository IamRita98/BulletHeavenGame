using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemyPrefab;
    Camera camera;
    float spawnBuffer = 0.2f;
    public Vector2 spawnPos;
   
    void Start()
    {
        camera = Camera.main.GetComponent<Camera>(); //(0,0)bot left and (1,1) top right
        for (int i = 0; i < 20; i++)
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

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
