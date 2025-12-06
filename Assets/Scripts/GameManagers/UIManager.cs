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
        healt,
        projectiles,
        globalDam,
    }
    void Start()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
    }
    public class UpgradeInfo
    {
        public Dictionary<int,string> descriptions;
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
                        { 0,"+5% weapon damage upgrade!"},
                        {1,"+10%" },
                        {2,"+15% and +1 projectile!" }
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
                        {0,"+5% fire rate upgrade!" },
                        {1,"+10% fire rate upgrade!" },
                        {2,"15% fire rate and +1 projectile!" }
                    }
                }
            },
            //add more here
        };
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
