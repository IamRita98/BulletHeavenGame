using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageOverTime : MonoBehaviour
{
    CombatHandler combatHandler;
    EnemyBaseStats ebs;
    float defaultProcTimer = 1f / 3f;
    float timer;
    float procTimer;
    bool currentlyDebuffed;
    float damage = 0;
    float duration = 0;
    public DoTType dType;

    public enum DoTType
    {
        burn,
        poison
    }
    private void Awake()
    {
        ebs = GetComponent<EnemyBaseStats>();
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
    }

    /// <summary>
    /// Pass Damage, Duration, & DamageOverTime DotType to be applied to target GO
    /// </summary>
    /// <param name="doTDamage"></param>
    /// <param name="doTDuration"></param>
    /// <param name="dotType"></param>
    public void ApplyDamageOverTimeEffect(float doTDamage, float doTDuration, DamageOverTime.DoTType dotType)
    {
        dType = dotType;
        switch (dType)
        {
            case DoTType.burn:
                duration += doTDuration;
                damage += doTDamage;
                currentlyDebuffed = true;
                break;

            case DoTType.poison:
                if (currentlyDebuffed && doTDuration > duration)
                {
                    timer = 0;
                    duration = doTDuration;
                }
                else if (currentlyDebuffed && doTDamage > damage)
                {
                    damage = doTDamage;
                }
                else if (!currentlyDebuffed)
                {
                    duration = doTDuration;
                    damage = doTDamage;
                    currentlyDebuffed = true;
                }
                    break;
        }

    }

    private void Update()
    {
        if (!currentlyDebuffed) return;
        timer += Time.deltaTime;
        procTimer += Time.deltaTime;

        if (procTimer > defaultProcTimer)
        {
            ApplyDoT();
            procTimer = 0;
        }

        if (timer > duration) RemoveDoT();
    }

    void ApplyDoT()
    {
        if (dType == DoTType.poison)
        {
            combatHandler.HandleDamage(damage, gameObject, CombatHandler.DamageType.Poison);
        }
        else if (dType == DoTType.burn)
        {
            print("Burn Dmg: " + damage);
            combatHandler.HandleDamage(damage, gameObject, CombatHandler.DamageType.Fire);
        }
            
    }

    void RemoveDoT() 
    {
        timer = 0;
        currentlyDebuffed = false;
        damage = 0;
        duration = 0;
    }
}
