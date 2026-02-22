using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class DefaultDanielAbility3Behaviour : MonoBehaviour
{
    GenericBuffing gb;
    GameObject player;
    GameObject weapon;
    DDAbility3SupportFire ability3SupportFire;
    bool once=false;
    float lifeTime;
    BaseWeaponStats bws;
    float baseFireRate;
    float newFireRate;
    float buffValueChanged;
    float attackSpeedBuffToApply = .5f;
    float pierceBuffToApply = 10;
    float projectileBuffToApply = 10;
    float abilityStrength=1;//implement a way to scale the buff (upgrade system)
    SpriteRenderer ability3Renderer;
    CircleCollider2D ability3Circle;
    public bool path3Tier1 = false;
    public bool path3Tier3 = false;
    bool buffIsApplied = false;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        bws = weapon.GetComponent<BaseWeaponStats>();
        gb = GetComponent<GenericBuffing>();
        ability3Renderer = gameObject.GetComponent<SpriteRenderer>();
        ability3Circle = gameObject.GetComponent<CircleCollider2D>();
        ability3Circle.enabled = false;
        ability3Renderer.enabled = false;
        ability3SupportFire = gameObject.GetComponent<DDAbility3SupportFire>();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += Deactivate;
        if (SceneManager.GetActiveScene().name == "MainMenu") return;
        //abilityStrength;
        transform.position = player.transform.position;
        if (path3Tier1) ApplyBuff();
        else transform.parent = null;
        lifeTime = 0;
        ability3Circle.enabled = true;
        ability3Renderer.enabled = true;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= Deactivate;
    }

    void Deactivate(Scene oldScene, Scene newScene)
    {
        if(newScene.name == "MainMenu") return;
        gameObject.SetActive(false);
    }

    private void Update()
    {   
        lifeTime += Time.deltaTime;
        if (lifeTime >= gameObject.GetComponent<AbilityStats>().LifeTime.StatsValue())
        {
            UnapplyBuff();
            gameObject.transform.parent = player.transform;
            gameObject.SetActive(false);
        }
    }

    //Maybe apply an ability Strength effect onto the buffs, we'll need to work out how to apply it though w/ something like buffToApply * Abilitystrenght * .8 or something
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        ApplyBuff();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        UnapplyBuff();
    }

    void ApplyBuff()
    {
        if (buffIsApplied) return;
        buffIsApplied = true;
        print("Gained Buff");
        gb.BuffStat(weapon, projectileBuffToApply, "weapon", "projectile");
        gb.BuffStat(weapon, pierceBuffToApply, "weapon", "pierce");
        gb.BuffStat(weapon, attackSpeedBuffToApply, "weapon", "rate");
    }
     void UnapplyBuff()
    {
        if (!buffIsApplied) return;
        buffIsApplied = false;
        print("Lost Buff");
        gb.BuffStat(weapon, -projectileBuffToApply, "weapon", "projectile");
        gb.BuffStat(weapon, -pierceBuffToApply, "weapon", "pierce");
        bws.AttackRate.AddMultiValue(1 / attackSpeedBuffToApply);
    }

}
