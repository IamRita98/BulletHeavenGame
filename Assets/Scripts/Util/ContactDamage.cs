using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    public static event System.Action<float, GameObject> OnCollision;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        EnemyBaseStats ebs = collision.gameObject.GetComponent<EnemyBaseStats>();
        float damage = ebs.ContactDamage.StatsValue();
        OnCollision?.Invoke(damage, gameObject);
    }
}
