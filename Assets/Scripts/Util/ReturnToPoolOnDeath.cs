using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPoolOnDeath : MonoBehaviour
{
    
    ObjectPooling oPool;
    private void Awake()
    {
        oPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPooling>();
    }
    public void ReturnToPool()
    {
        oPool.objectPool.Add(gameObject);
        oPool.activePool.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
