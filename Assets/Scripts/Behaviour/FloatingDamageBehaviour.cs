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
    private void Update()
    {
        if (!isColored)
        {
            isColored = true;
            PickColor();
        }
        timer += Time.deltaTime;
        if (timer>= lifeSpan)
        {
            GameObject temp= oPool.activePool[0];
            oPool.activePool.Remove(temp);
            oPool.objectPool.Add(temp);
            temp.transform.SetParent(oPool.transform);
            temp.SetActive(false);
            isColored = false;
            timer = 0;
        }
        rb.velocity = (direction * speed) * Time.deltaTime;
    }
    void PickColor()
    {
        switch (cHandler.dType)
        {
            case CombatHandler.DamageType.Physical:
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
                number.color = Color.gray;
                break;
        }
    }
}
