using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ability3Behaviour : MonoBehaviour
{
    float buffAmount;
    float buffTime;
    GenericBuffing gb;
    GameObject player;
    GameObject weapon;
    bool isInCircle;
    float lifeTime;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        gb = GetComponent<GenericBuffing>();
        
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
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
        print("In trigger");
        gb.BuffStat(weapon, 10f, "weapon", "projectile");
        gb.BuffStat(weapon, 10f, "weapon", "pierce");
        gb.BuffStat(weapon, -.5f, "weapon", "rate");
        print("New Rate: " + weapon.GetComponent<BaseWeaponStats>().AttackRate.StatsValue());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        print("Out trigger");
        gb.BuffStat(weapon, -10f, "weapon", "projectile");
        gb.BuffStat(weapon, -10f, "weapon", "pierce");
        gb.BuffStat(weapon, .2f, "weapon", "rate");
    }
}
