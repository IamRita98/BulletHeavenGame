using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingDamageBehaviour : MonoBehaviour
{
    ObjectPooling oPool;
    Vector3 direction = new Vector3(-2f, 1f,0);
    Rigidbody rb;
    public float speed;
    public float lifeSpan = .5f;
    float spawnTime;
    public TMP_Text number;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        oPool = GameObject.FindGameObjectWithTag ("FloatingDamageNumbersPool").GetComponent<ObjectPooling>();
    }
    private void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
        {
            GameObject temp= oPool.activePool[0];
            oPool.activePool.Remove(temp);
            oPool.objectPool.Add(temp);
            temp.transform.SetParent(oPool.transform);
            temp.SetActive(false);
        }
        rb.velocity = (direction * speed) * Time.deltaTime;
    }
}
