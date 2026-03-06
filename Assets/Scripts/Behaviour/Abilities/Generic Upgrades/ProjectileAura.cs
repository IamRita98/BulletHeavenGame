using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileAura : MonoBehaviour
{
    float projDam;
    CombatHandler cHandler;
    ProjectileBehaviour pBehaviour;
    float tickTimer = 1 / 3f;
    float timer = 0;
    bool tickDamage=false;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu") GetReferences();
    }
    void GetReferences()
    {
        cHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
    }
    public void SetAuraStats(float damage)
    {
        projDam = damage;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        timer++;
        if (timer >= tickTimer) tickDamage = true;
        if (tickDamage)
        {
            float dam = projDam / 5;
            tickDamage = false;
            timer = 0;
            print("AuraDmg:" + dam);
            cHandler.HandleDamage(dam, collision.gameObject, CombatHandler.DamageType.Untyped);
        }

    }
}
