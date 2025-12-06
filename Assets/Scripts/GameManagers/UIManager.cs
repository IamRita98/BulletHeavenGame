using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    BaseStats playerBStats;
    BaseWeaponStats baseWeaponStats;
    public List<Button> upgradeButtonList;
    void Start()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
    }
    public void GetUpgradeInfo(string upgrade)
    {
        int tier;
        switch (upgrade)
        {
            case "weaponDam":
                tier = baseWeaponStats.bDamUpgT;
                switch (tier)
                {
                    case 0:
                        string tier1Desc = "+5% to your weapon damage!";
                        DisplayUpgrade(upgrade, tier, tier1Desc);
                        break;
                }
                break;
            case "globalDam":
                tier = playerBStats.globalDamT;
                break;
            case "fireRate":
                tier = baseWeaponStats.attackRUpgT;
                break;
            case "health":
                tier = playerBStats.healthUpgT;
                break;
            case "projectile":
                tier = playerBStats.projUpgT;
                break;
            case "weapArea":
                tier = baseWeaponStats.weapAreaUpgT;
                break;
        }
    }
    void DisplayUpgrade(string upgrade, int tier, string upgrDesc)
    {

    }
}
