using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
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

    public void RollUpgrades()
    {
        //string[] rolledUpg=new string[possibleChoices];
        List<int> rolledUpgrades = new List<int>();
        for (int i = 0; i < possibleChoices; i++)
        {
            int rolled = Random.Range(0, upgradeArr.Length);
            while(rolledUpgrades.Contains(rolled)) rolled = Random.Range(0, upgradeArr.Length);
            rolledUpgrades.Add(rolled);
            print("rolled: " + rolled);
            uIManager.GetUpgradeInfo(upgradeArr[rolled], i);
        }
    }

}
