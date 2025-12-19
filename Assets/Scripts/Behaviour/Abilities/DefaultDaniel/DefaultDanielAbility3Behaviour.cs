using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultDanielAbility3Behaviour : MonoBehaviour
{
    GenericBuffing gb;
    GameObject player;
    GameObject weapon;
    float lifeTime;
    BaseWeaponStats bws;
    float baseFireRate;
    float newFireRate;
    float buffValueChanged;
    float attackSpeedBuffToApply = .5f;
    float pierceBuffToApply = 10;
    float projectileBuffToApply = 10;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        bws = weapon.GetComponent<BaseWeaponStats>();
        gb = GetComponent<GenericBuffing>();
        
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        transform.position = player.transform.position;
        transform.parent = null;
        lifeTime = 0;
    }
    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= gameObject.GetComponent<AbilityStats>().LifeTime.StatsValue())
        {
            gameObject.transform.parent = player.transform;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Old Rate: " + weapon.GetComponent<BaseWeaponStats>().AttackRate.StatsValue());
        if (!collision.CompareTag("Player")) return;
        gb.BuffStat(weapon, projectileBuffToApply, "weapon", "projectile");
        gb.BuffStat(weapon, pierceBuffToApply, "weapon", "pierce");
        gb.BuffStat(weapon, attackSpeedBuffToApply, "weapon", "rate");
        print("New Rate: " + weapon.GetComponent<BaseWeaponStats>().AttackRate.StatsValue());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        gb.BuffStat(weapon, -projectileBuffToApply, "weapon", "projectile");
        gb.BuffStat(weapon, -pierceBuffToApply, "weapon", "pierce");
        bws.AttackRate.AddMultiValue(1/attackSpeedBuffToApply);
        print("FireRate returned to: " +bws.AttackRate.StatsValue());
    }
}
