using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UIManager;
public class UpgradeManager : MonoBehaviour
{
    public Button WeapDamageButton;
    BaseStats playerBStats;
    BaseWeaponStats baseWeaponStats;
    int possibleChoices = 3;
    string[] upgradeArr = { "weaponDam", "globalDam", "fireRate", "health", "projectile", "weapArea" };
    UIManager uIManager;

    public enum UpgradeTypes
    {
        weapDam,
        weapFireRate,
        weapArea,
        health,
        projectiles,
        globalDam,
    }


    private void Awake()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        InitializeUpgrades();
    }

    public void RollUpgrades()
    {
        //string[] rolledUpg=new string[possibleChoices];
        List<int> rolledUpgrades = new List<int>();
        for (int i = 0; i < possibleChoices; i++)
        {
            int rolled = UnityEngine.Random.Range(0, upgradeArr.Length);
            while (rolledUpgrades.Contains(rolled)) rolled = UnityEngine.Random.Range(0, upgradeArr.Length);
            rolledUpgrades.Add(rolled);
            print("rolled: " + rolled);
            GetUpgradeInfo(upgradeArr[rolled], i);
        }
    }

    public class UpgradeInfo
    {
        public Dictionary<int, string> descriptions;
        public Func<int> GetTier;
    }
    private Dictionary<UpgradeTypes, UpgradeInfo> upgrades;
    private void InitializeUpgrades()
    {
        upgrades = new Dictionary<UpgradeTypes, UpgradeInfo>
        {
            {
                UpgradeTypes.weapDam,
                new UpgradeInfo{
                    GetTier = () => baseWeaponStats.bDamUpgT,
                    descriptions = new Dictionary<int, string>
                    {
                        {0,"Weapon Damage \n 15% Damage \n Flavor text"},
                        {1,"+10% Damage, +10% Area" },
                        {2,"Enemies have a 25% chance of exploding for 30% of the damage of the killing blow" },
                        {3,"+40% Damage, +1 Projectiles, -10% Firerate" }
                    }
                }
            },
            {
                UpgradeTypes.globalDam,
                new UpgradeInfo{
                    GetTier = () => playerBStats.globalDamT,
                    descriptions = new Dictionary<int, string>
                    {
                        {0,"+15% Global Damage"},
                        {1,"+10% Global Damage, +10% Area" },
                        {2,"Enemies have a 25% chance of exploding for 30% of the damage of the killing blow" },
                        {3,"+40% Damage, +1 Projectiles, -10% Firerate" }
                    }
                }
            },
            {
                UpgradeTypes.weapFireRate,
                new UpgradeInfo
                {
                    GetTier = () => baseWeaponStats.attackRUpgT,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"+15% Firerate" },
                        {1,"+1 projectile, +10% Firerate" },
                        {2,"+60% Firerate, -10% Damage" },
                        {3, "+1 Projectile, +10% Firerate, each subsequent hit on an enemy adds +1 damage(max 50 stacks)" }
                    }
                }
            },
            {
                UpgradeTypes.weapArea,
                new UpgradeInfo
                {
                    GetTier = () => baseWeaponStats.weapAreaUpgT,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"+5% Area" },
                        {1,"+10% Area" },
                        {2,"15% Area" },
                        {3,"15% Area" }
                    }
                }
            },
            {
                UpgradeTypes.projectiles,
                new UpgradeInfo
                {
                    GetTier = () => playerBStats.projUpgT,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"+1 Proj" },
                        {1,"+1 Proj" },
                        {2,"+1 Proj" },
                        {3,"+1 Proj" }
                    }
                }
            },
            {
                UpgradeTypes.health,
                new UpgradeInfo
                {
                    GetTier = () => playerBStats.healthUpgT,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"+10 HP" },
                        {1,"+20 HP" },
                        {2,"+30 HP" },
                        {3, "+30 HP" }
                    }
                }
            },
            //add more here
        };
    }
    public void GetUpgradeInfo(string upgrade, int upgradeButton)
    {
        switch (upgrade)
        {
            case "weaponDam":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.weapDam], upgradeButton, UpgradeTypes.weapDam);
                break;
            case "globalDam":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.globalDam], upgradeButton, UpgradeTypes.globalDam);
                break;
            case "fireRate":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.weapFireRate], upgradeButton, UpgradeTypes.weapFireRate);
                break;
            case "health":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.health], upgradeButton, UpgradeTypes.health);
                break;
            case "projectile":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.projectiles], upgradeButton, UpgradeTypes.projectiles);
                break;
            case "weapArea":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.weapArea], upgradeButton, UpgradeTypes.weapArea);
                break;
        }
    }

    public void ApplyUpgrades(int tier, UpgradeManager.UpgradeTypes upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeManager.UpgradeTypes.weapDam:
                switch (tier)
                {
                    case 0:
                        baseWeaponStats.BaseDamage.AddMultiValue(.15f);
                        print("Weap dmg "  +baseWeaponStats.BaseDamage.StatsValue());
                        baseWeaponStats.bDamUpgT++;
                        break;
                    case 1:
                        baseWeaponStats.BaseDamage.AddMultiValue(.10f);
                        baseWeaponStats.WeapArea.AddMultiValue(.10f);
                        print("Weap dmg " + baseWeaponStats.BaseDamage.StatsValue());
                        baseWeaponStats.bDamUpgT++;
                        break;
                    case 2:
                        baseWeaponStats.BaseDamage.AddMultiValue(.40f);
                        playerBStats.Projectiles.AddFlatValue(1f);
                        baseWeaponStats.AttackRate.AddMultiValue(-.10f);
                        print("Weap dmg " + baseWeaponStats.BaseDamage.StatsValue());
                        baseWeaponStats.bDamUpgT++;
                        break;
                    case 3:
                        baseWeaponStats.bDamUpgT++;
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.weapArea:
                switch (tier)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.projectiles:
                switch (tier)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.weapFireRate:
                switch (tier)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.globalDam:
                switch (tier)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.health:
                switch (tier)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
        }
        uIManager.HideUpgrades();
    }
}
