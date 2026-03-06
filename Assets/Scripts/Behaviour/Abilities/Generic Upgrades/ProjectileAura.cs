using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileAura : MonoBehaviour
{
    float dam;
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
        dam = damage;
    }
    private void Update()
    {
        timer++;
        if (timer >= tickTimer) tickDamage = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        
        if (tickDamage)
        {
            tickDamage = false;
            timer = 0;
            cHandler.HandleDamage(dam, collision.gameObject, CombatHandler.DamageType.Untyped);
        }

    }
}
