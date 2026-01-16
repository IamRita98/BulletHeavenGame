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
    bool once=false;
    float lifeTime;
    BaseWeaponStats bws;
    float baseFireRate;
    float newFireRate;
    float buffValueChanged;
    float attackSpeedBuffToApply = .5f;
    float pierceBuffToApply = 10;
    float projectileBuffToApply = 10;
    SpriteRenderer ability3Renderer;
    CircleCollider2D ability3Circle;


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
    }
    private void Start()
    {

        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;
        transform.position = player.transform.position;
        transform.parent = null;
        lifeTime = 0;
        ability3Circle.enabled = true;
        ability3Renderer.enabled = true;
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
        if (!collision.CompareTag("Player")) return;
        gb.BuffStat(weapon, projectileBuffToApply, "weapon", "projectile");
        gb.BuffStat(weapon, pierceBuffToApply, "weapon", "pierce");
        gb.BuffStat(weapon, attackSpeedBuffToApply, "weapon", "rate");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        gb.BuffStat(weapon, -projectileBuffToApply, "weapon", "projectile");
        gb.BuffStat(weapon, -pierceBuffToApply, "weapon", "pierce");
        bws.AttackRate.AddMultiValue(1/attackSpeedBuffToApply);
    }
}
