using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericOnEnterDam : MonoBehaviour
{
    CombatHandler cH;
    private void Awake()
    {
        cH = gameObject.GetComponent<CombatHandler>();
    }
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        cH.HandleDamage(damage, collision.gameObject);
    }
}
