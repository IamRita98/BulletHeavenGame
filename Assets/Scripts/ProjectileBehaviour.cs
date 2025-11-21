using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    ObjectPooling oPool;
    [SerializeField]float speed;
    float lifeTime;
    float timer = 0;

    private void Start()
    {
        oPool = gameObject.transform.parent.GetComponent<ObjectPooling>();
    }

    private void Update()
    {
        if(timer >= lifeTime)
        {
            ReturnToPool();
            timer = 0;
        }
        transform.Translate((Vector2.up * speed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        //DealDamage
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        oPool.objectPool.Add(gameObject);
        oPool.activePool.Remove(gameObject);
        gameObject.SetActive(false);
    }
}
