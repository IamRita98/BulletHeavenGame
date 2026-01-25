using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SarahBuffAbility : MonoBehaviour
{
    GenericBuffing genericBuffing;
    GameObject weaponSword;
    BaseWeaponStats bws;
    GameObject player;
    GameObject ability1;
    GameObject ability2;
    float buffAmount=2;
    string buffType="weapon";
    string buffStat="damage";
    string abilityBuff = "ability";
    float buffDuration;
    float timer=0;
    bool isBuffed;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponSword = GameObject.FindGameObjectWithTag("Weapon");
        ability1 = GameObject.FindGameObjectWithTag("Ability1");
        ability2 = GameObject.FindGameObjectWithTag("Ability2");
        genericBuffing = gameObject.GetComponent<GenericBuffing>();
        bws = GameObject.FindGameObjectWithTag("Weapon").GetComponent<BaseWeaponStats>();
    }
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") return;
        timer = 0;
        isBuffed = true;
        ApplyBuffs();
    }
    private void ApplyBuffs()
    {
        print("auto attack before buff: " + bws.BaseDamage.StatsValue());
        genericBuffing.BuffStat(weaponSword,buffAmount,buffType,buffStat);//damage buff to autoattacks
        genericBuffing.BuffStat(ability1, buffAmount, abilityBuff, buffStat);//damage buff to ability1
        print("auto attack buffed to: "+bws.BaseDamage.StatsValue());
    }
    private void RemoveBuffs()
    {
        genericBuffing.BuffStat(weaponSword, buffAmount/2, buffType, buffStat);//damage buff to autoattacks
        genericBuffing.BuffStat(ability1, buffAmount/2, abilityBuff, buffStat);//damage buff to ability1
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= buffDuration)//&&isBuffed put back in if buggy
        {
            isBuffed = false;
            RemoveBuffs();
            timer = 0;
        }
    }
}
