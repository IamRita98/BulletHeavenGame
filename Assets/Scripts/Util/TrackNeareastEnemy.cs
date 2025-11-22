using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TrackNeareastEnemy : MonoBehaviour
{
    ObjectPooling oPool;
    private GameObject nearestEnemyV2;
    float test1 = 0f;
    void Start()
    {
        oPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPooling>();
    }


    public GameObject NearestEnemy()
    {
        float dist=Mathf.Infinity;
        if (oPool.activePool.Count <= 1) return null;

        for (int i = 0; i < oPool.activePool.Count; i++)//there may be a more efficient way (2b optimized)
        {
            Vector2 directionToTarget = oPool.activePool[i].transform.position - gameObject.transform.position;
            float dSqrTT = directionToTarget.sqrMagnitude;
            if (dSqrTT< dist)
            {
                dist = dSqrTT;
                nearestEnemyV2 = oPool.activePool[i];
            }
        }
        return nearestEnemyV2;
    }
}
