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
    float timer = 0;
    float spawnTime;
    public TMP_Text number;
    bool isColored=false;
    CombatHandler cHandler;
    


    private void Start()
    {
        cHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        rb = GetComponent<Rigidbody>();
        oPool = GameObject.FindGameObjectWithTag ("FloatingDamageNumbersPool").GetComponent<ObjectPooling>();
        number = gameObject.GetComponent<TMP_Text>();
    }
    public void Setup(TMP_Text text, CombatHandler.DamageType type)
    {
        number = text;
        PickColor(type);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>= lifeSpan)
        {
            oPool.activePool.Remove(gameObject);
            oPool.objectPool.Add(gameObject);
            //gameObject.transform.SetParent(oPool.transform);
            gameObject.SetActive(false);
            isColored = false;
            timer = 0;
        }
        rb.velocity = (direction * speed) * Time.deltaTime;
    }
    void PickColor(CombatHandler.DamageType type)
    {
        switch (type)
        {
            case CombatHandler.DamageType.Physical:
                number.color = Color.white;
                break;
            case CombatHandler.DamageType.Poison:
                number.color = Color.green;
                break;
            case CombatHandler.DamageType.Fire:
                number.color = Color.red;
                break;
            case CombatHandler.DamageType.Light:
                number.color = Color.yellow;
                break;
            case CombatHandler.DamageType.Untyped:
                number.color = Color.blue;
                break;
        }
    }
}
