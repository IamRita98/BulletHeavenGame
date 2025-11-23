using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject poolObject;
    [SerializeField] int objectAmount=1;
    public List<GameObject> objectPool = new List<GameObject>();
    public List<GameObject> activePool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < objectAmount; i++)
        {
            GameObject spawnedGO = Instantiate(poolObject);
            objectPool.Add(spawnedGO);
            spawnedGO.transform.parent = gameObject.transform;
            spawnedGO.SetActive(false);
        }    
    }
}
