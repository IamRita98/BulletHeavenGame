using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    BaseStats playerBStats;
    BaseWeaponStats baseWeaponStats;
    public List<Button> upgradeButtonList;
    public enum UpgradeTypes
    {
        weapDam,
        weapFireRate,
        weapArea,
        health,
        projectiles,
        globalDam,
    }
    void Start()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
        InitializeUpgrades();
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
                        {0,"15% Damage"},
                        {1,"+10% Damage, +10% Area" },
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
    public void GetUpgradeInfo(string upgrade)
    {
        switch (upgrade)
        {
            case "weaponDam":
                DisplayUpgrade(upgrades[UpgradeTypes.weapDam]);
                break;
            case "globalDam":
                DisplayUpgrade(upgrades[UpgradeTypes.globalDam]);
                break;
            case "fireRate":
                DisplayUpgrade(upgrades[UpgradeTypes.weapFireRate]);
                break;
            case "health":
                DisplayUpgrade(upgrades[UpgradeTypes.health]);
                break;
            case "projectile":
                DisplayUpgrade(upgrades[UpgradeTypes.projectiles]);
                break;
            case "weapArea":
                DisplayUpgrade(upgrades[UpgradeTypes.weapArea]);
                break;
        }
    }
    void DisplayUpgrade(UpgradeInfo upgradeInfo)
    {
        
    }
}
