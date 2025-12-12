using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UpgradeManager : MonoBehaviour
{
    public Button WeapDamageButton;
    BaseStats playerBStats;
    BaseWeaponStats baseWeaponStats;
    int possibleChoices = 3;
    string[] upgradeArr = {"weaponDam","globalDam","fireRate","health","projectile","weapArea"};
    UIManager uIManager;
    
    
    private void Awake()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
    }
    void WeaponDamageUpgrade()
    {

    }
    public void RollUpgrades()
    {
        //string[] rolledUpg=new string[possibleChoices];
        System.Random random = new();
        for(int i = 0; i < possibleChoices; i++)
        {
            int rolled = random.Next(upgradeArr.Length);
            print("rolled: " + rolled);
            uIManager.GetUpgradeInfo(upgradeArr[rolled],i);
        }
    }

    void UpgradeTier(string key,int tier)
    {
        
            
    }
    
}
