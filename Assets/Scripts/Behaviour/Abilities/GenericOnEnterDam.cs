using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericOnEnterDam : MonoBehaviour
{
    CombatHandler cH;
    private void Awake()
    {
        cH = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
    }
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cH.HandleDamage(damage, collision.gameObject);
        print("Generic dam: " + damage);
    }
}
