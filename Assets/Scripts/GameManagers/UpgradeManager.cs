using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using static UIManager;
public class UpgradeManager : MonoBehaviour
{
    /// <summary>
    /// Event Called on Levelup to Update values
    /// </summary>
    public static event System.Action OnLevelUp;

    public SpritesReferenceSO spriteReferences;
    public Button WeapDamageButton;
    GameObject ability1GO;
    GameObject ability2GO;
    GameObject ability3GO;
    BaseStats playerBStats;
    BaseWeaponStats baseWeaponStats;
    AbilityStats ability1;
    AbilityStats ability2;
    AbilityStats ability3;
    int possibleChoices = 3;
    List<string> upgradeArr = new List<string> { "weaponDam", "globalDam", "fireRate", "health", "projectile", "weapArea" };
    List<string> defaultDaniel = new List<string> { "DDability1Path1", "DDability1Path2", "DDability1Path3", "DDability2Path1", "DDability2Path2", "DDability2Path3", "DDability3Path1", 
                                                    "DDability3Path2" , "DDability3Path3" };
    GameObject playerCharacter;
    AbilityStats[] abilities;

    UIManager uIManager;
    public enum UpgradeTypes
    {   //global upgrades
        weapDam,
        weapFireRate,
        weapArea,
        health,
        projectiles,
        globalDam,

        //character specific upgrades
        //defaultDaniel
        defaultDanielAbility1Path1,
        defaultDanielAbility1Path2,
        defaultDanielAbility1Path3,
        defaultDanielAbility2Path1,
        defaultDanielAbility2Path2,
        defaultDanielAbility2Path3,
        defaultDanielAbility3Path1,
        defaultDanielAbility3Path2,
        defaultDanielAbility3Path3,

        //sarahSword
        sarahSwordAbility1,
    }


    private void Awake()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        GetPlayerCharacterAndAbilities();

        InitializeUpgrades();
    }

    void GetPlayerCharacterAndAbilities()
    {
        switch (playerBStats.characterSelected)
        {
            case BaseStats.Character.DefaultDaniel:
                upgradeArr.AddRange(defaultDaniel);
                break;
            case BaseStats.Character.SarahSword:

                break;
        }
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        abilities = playerCharacter.transform.GetComponentsInChildren<AbilityStats>(true);
        ability1 = abilities[0];
        ability2 = abilities[1];
        ability3 = abilities[2];
        ability1GO = GameObject.FindGameObjectWithTag("Ability1");
        ability2GO = GameObject.FindGameObjectWithTag("Ability2");
        ability3GO = GameObject.FindGameObjectWithTag("Ability3");
    }
    /*    private void OnEnable()
        {
            SceneManager.activeSceneChanged +=
        }*/

    public void RollUpgrades()
    {
        //string[] rolledUpg=new string[possibleChoices];
        List<int> rolledUpgrades = new List<int>();
        for (int i = 0; i < possibleChoices; i++)
        {
            int rolled = UnityEngine.Random.Range(0, upgradeArr.Count);
            while (rolledUpgrades.Contains(rolled)) rolled = UnityEngine.Random.Range(0, upgradeArr.Count);
            rolledUpgrades.Add(rolled);
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
                        {2,"+40% Damage, +1 Projectiles, -10% Firerate" },
                        {3,"Enemies have a 25% chance of exploding for 30% of the damage of the killing blow" },
                    }
                }
            },
            {
                UpgradeTypes.globalDam,
                new UpgradeInfo{
                    GetTier = () => playerBStats.globalDamT,
                    descriptions = new Dictionary<int, string>
                    {
                        {0,"+10% Global Damage"},
                        {1,"+15% Global Damage" },
                        {2,"+40% Global Damage" },
                        {3,"Enemies have a 25% chance of exploding for 30% of the damage of the killing blow" },
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
            {
                UpgradeTypes.defaultDanielAbility1Path1,
                new UpgradeInfo
                {
                    GetTier = () => ability1.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"Beam is mirrored"},
                        {1,"Two more beams" },
                        {2,"Beams rotate around you"}//if upgrades are buggy remember -> (we removed the "alt" tier 3)
                    }
                }
            },
                        {
                UpgradeTypes.defaultDanielAbility1Path2,
                new UpgradeInfo
                {
                    GetTier = () => ability1.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"Beam is buhh"},
                        {1,"Beam is even bigger but has increased CD" },
                        {2,"Beam burns enemies, killed enemies drop extra XP"}//if upgrades are buggy remember -> (we removed the "alt" tier 3)
                    }
                }
            },
                                    {
                UpgradeTypes.defaultDanielAbility1Path3,
                new UpgradeInfo
                {
                    GetTier = () => ability1.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"Beam is now an AOE"},
                        {1,"Slow enemies" },
                        {2,"AOE is persistent"}//if upgrades are buggy remember -> (we removed the "alt" tier 3)
                    }
                }
            },
            {
                UpgradeTypes.defaultDanielAbility2Path1,
                new UpgradeInfo
                {
                    GetTier = () => ability2.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"Place holder"},
                        {1,"projectiles return" },
                        {2,"etc..." },
                        {3, "+30 HP" }
                    }
                }
            },
                        {
                UpgradeTypes.defaultDanielAbility2Path2,
                new UpgradeInfo
                {
                    GetTier = () => ability2.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"Place holder"},
                        {1,"projectiles return" },
                        {2,"etc..." },
                        {3, "+30 HP" }
                    }
                }
            },
                                    {
                UpgradeTypes.defaultDanielAbility2Path3,
                new UpgradeInfo
                {
                    GetTier = () => ability2.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"Place holder"},
                        {1,"projectiles return" },
                        {2,"etc..." },
                        {3, "+30 HP" }
                    }
                }
            },
            {
                UpgradeTypes.defaultDanielAbility3Path1,
                new UpgradeInfo
                {
                    GetTier = () => ability3.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"longer duration"},
                        {1,"stronger buffs" },
                        {2,"etc..." },
                        {3, "+30 HP" }
                    }
                }
            },
                        {
                UpgradeTypes.defaultDanielAbility3Path2,
                new UpgradeInfo
                {
                    GetTier = () => ability3.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"longer duration"},
                        {1,"stronger buffs" },
                        {2,"etc..." },
                        {3, "+30 HP" }
                    }
                }
            },
                                    {
                UpgradeTypes.defaultDanielAbility3Path3,
                new UpgradeInfo
                {
                    GetTier = () => ability3.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"longer duration"},
                        {1,"stronger buffs" },
                        {2,"etc..." },
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
            case "DDability1Path1":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility1Path1], upgradeButton, UpgradeTypes.defaultDanielAbility1Path1);
                break;
            case "DDability1Path2":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility1Path2], upgradeButton, UpgradeTypes.defaultDanielAbility1Path2);
                break;
            case "DDability1Path3":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility1Path3], upgradeButton, UpgradeTypes.defaultDanielAbility1Path3);
                break;
            case "DDability2Path1":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility2Path1], upgradeButton, UpgradeTypes.defaultDanielAbility2Path1);
                break;
            case "DDability2Path2":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility2Path2], upgradeButton, UpgradeTypes.defaultDanielAbility2Path2);
                break;
            case "DDability2Path3":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility2Path3], upgradeButton, UpgradeTypes.defaultDanielAbility2Path3);
                break;
            case "DDability3Path1":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility3Path1], upgradeButton, UpgradeTypes.defaultDanielAbility3Path1);
                break;
            case "DDability3Path2":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility3Path2], upgradeButton, UpgradeTypes.defaultDanielAbility3Path2);
                break;
            case "DDability3Path3":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAbility3Path3], upgradeButton, UpgradeTypes.defaultDanielAbility3Path3);
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
                        baseWeaponStats.BaseDamage.AddMultiValue(1.15f);
                        print("Weap dmg " + baseWeaponStats.BaseDamage.StatsValue());
                        baseWeaponStats.bDamUpgT++;
                        break;
                    case 1:
                        baseWeaponStats.BaseDamage.AddMultiValue(1.10f);
                        baseWeaponStats.WeapArea.AddMultiValue(1.10f);
                        print("Weap dmg " + baseWeaponStats.BaseDamage.StatsValue());
                        print("Weap Area " + baseWeaponStats.WeapArea.StatsValue());
                        baseWeaponStats.bDamUpgT++;
                        break;
                    case 2:
                        baseWeaponStats.BaseDamage.AddMultiValue(1.40f);
                        playerBStats.Projectiles.AddFlatValue(1f);
                        baseWeaponStats.AttackRate.AddMultiValue(.90f);
                        print("Weap dmg " + baseWeaponStats.BaseDamage.StatsValue());
                        print("Projectiles " + playerBStats.Projectiles.StatsValue());
                        print("Fire rate " + baseWeaponStats.AttackRate.StatsValue());
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
                        baseWeaponStats.WeapArea.AddMultiValue(1.10f);
                        print("Weap Area " + baseWeaponStats.WeapArea.StatsValue());
                        baseWeaponStats.weapAreaUpgT++;
                        break;
                    case 1:
                        baseWeaponStats.WeapArea.AddMultiValue(1.15f);
                        print("Weap Area " + baseWeaponStats.WeapArea.StatsValue());
                        baseWeaponStats.weapAreaUpgT++;
                        break;
                    case 2:
                        baseWeaponStats.WeapArea.AddMultiValue(1.20f);
                        print("Weap Area " + baseWeaponStats.WeapArea.StatsValue());
                        baseWeaponStats.weapAreaUpgT++;
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.projectiles:
                switch (tier)
                {
                    case 0:
                        playerBStats.Projectiles.AddFlatValue(1f);
                        print("Projectiles " + playerBStats.Projectiles.StatsValue());
                        playerBStats.projUpgT++;
                        break;
                    case 1:
                        playerBStats.Projectiles.AddFlatValue(1f);
                        print("Projectiles " + playerBStats.Projectiles.StatsValue());
                        playerBStats.projUpgT++;
                        break;
                    case 2:
                        playerBStats.Projectiles.AddFlatValue(1f);
                        print("Projectiles " + playerBStats.Projectiles.StatsValue());
                        playerBStats.projUpgT++;
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.weapFireRate:
                switch (tier)
                {
                    case 0:
                        baseWeaponStats.AttackRate.AddMultiValue(.85f);
                        print("Fire rate " + baseWeaponStats.AttackRate.StatsValue());
                        baseWeaponStats.attackRUpgT++;
                        break;
                    case 1:
                        baseWeaponStats.AttackRate.AddMultiValue(.90f);
                        playerBStats.Projectiles.AddFlatValue(1);
                        print("Fire rate " + baseWeaponStats.AttackRate.StatsValue());
                        print("Projectiles " + playerBStats.Projectiles.StatsValue());
                        baseWeaponStats.attackRUpgT++;
                        break;
                    case 2:
                        baseWeaponStats.AttackRate.AddMultiValue(.60f);
                        baseWeaponStats.BaseDamage.AddMultiValue(.90f);
                        print("Fire rate " + baseWeaponStats.AttackRate.StatsValue());
                        print("Weap dmg " + baseWeaponStats.BaseDamage.StatsValue());
                        baseWeaponStats.attackRUpgT++;
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.globalDam: //Increment both Weap dam & all ability dam, then increment the tier stat in base stats
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
                        playerBStats.Health.AddFlatValue(10);
                        playerBStats.MaxHealth.AddFlatValue(10);
                        print("Max HP " + playerBStats.MaxHealth.StatsValue());
                        break;
                    case 1:
                        playerBStats.Health.AddFlatValue(20);
                        playerBStats.MaxHealth.AddFlatValue(20);
                        print("Max HP " + playerBStats.MaxHealth.StatsValue());
                        break;
                    case 2:
                        playerBStats.Health.AddFlatValue(30);
                        playerBStats.MaxHealth.AddFlatValue(30);
                        print("Max HP " + playerBStats.MaxHealth.StatsValue());
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility1Path1:
                DanielBeamBehaviur ability1Behav= ability1GO.GetComponent<DanielBeamBehaviur>();
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability1Path2");
                        upgradeArr.Remove("DDability1Path3");
                        ability1.BaseDamage.AddFlatValue(5);
                        ability1Behav.p1Tier1 = true;
                        print("Beam is mirrored " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 1:
                        ability1.BaseDamage.AddFlatValue(5);//2 more beams + shaped
                        ability1Behav.p1Tier2 = true;
                        print("2 more beams" + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 2:
                        ability1.BaseDamage.AddFlatValue(5);//beams spin
                        ability1Behav.p1Tier3 = true;
                        print("Beams rotate" + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        upgradeArr.Remove("DDability1Path1");
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility1Path2:
                DanielBeamBehaviur ability1Behaviour = ability1GO.GetComponent<DanielBeamBehaviur>();
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability1Path1");
                        upgradeArr.Remove("DDability1Path3");
                        ability1.BaseDamage.AddFlatValue(5);
                        ability1.Area.AddMultiValue(1.2f);
                        print("Beam is stronger " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 1:
                        ability1.BaseDamage.AddFlatValue(10);
                        ability1.Area.AddMultiValue(1.3f);
                        ability1.Cooldown.AddFlatValue(3);
                        ability1Behaviour.path2Tier2 = true;
                        print("Beam is much stronger but has a higher cd " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 2:
                        print("Enemies hit by beam burn-enemies killed by beam drop bonus XP " + ability1.BaseDamage.StatsValue());
                        ability1Behaviour.path2Tier3 = true;
                        ability1.upgradeTier++;
                        upgradeArr.Remove("DDability1Path2");
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility1Path3:
                AltBeamCircleBehaviour altBeam = ability1GO.GetComponent<AltBeamCircleBehaviour>();
                float damageReductionToBeam = -9f;
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability1Path2");
                        upgradeArr.Remove("DDability1Path1");
                        ability1.BaseDamage.AddFlatValue(damageReductionToBeam);
                        print("Beam is now an AOE around you" + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        ability1.LifeTime.AddFlatValue(1);
                        DanielBeamBehaviur danielBeam = ability1GO.GetComponent<DanielBeamBehaviur>();
                        Destroy(danielBeam);
                        ability1GO.AddComponent<AltBeamCircleBehaviour>();
                        SpriteRenderer ability1Sprite = ability1GO.GetComponent<SpriteRenderer>();
                        ability1Sprite.sprite = spriteReferences.beamCircleSprite;
                        break;
                    case 1:
                        print("Slow Enemies" + ability1.BaseDamage.StatsValue());
                        altBeam.tier2 = true;
                        ability1.upgradeTier++;
                        break;
                    case 2:
                        print("AOE is persistent " + ability1.BaseDamage.StatsValue());
                        altBeam.tier3 = true;
                        altBeam.gameObject.SetActive(true);
                        ability1.upgradeTier++;
                        upgradeArr.Remove("DDability1Path3");
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility2Path1:
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability2Path2");
                        upgradeArr.Remove("DDability2Path3");
                        ability1.BaseDamage.AddFlatValue(5);
                        print("Max Dam ability1 " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 1:
                        ability1.BaseDamage.AddFlatValue(5);//split
                        print("Max Dam " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 2:
                        ability1.BaseDamage.AddFlatValue(5);//burn
                        print("Max Dam " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility2Path2:
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability2Path1");
                        upgradeArr.Remove("DDability2Path3");
                        ability2.BaseDamage.AddFlatValue(5);
                        print("Max Dam ability 2 " + ability2.BaseDamage.StatsValue());
                        ability2.upgradeTier++;
                        break;
                    case 1:
                        ability2.BaseDamage.AddFlatValue(5);//split
                        print("Max Dam " + ability2.BaseDamage.StatsValue());
                        ability2.upgradeTier++;
                        break;
                    case 2:
                        ability2.BaseDamage.AddFlatValue(5);//burn
                        print("Max Dam " + ability2.BaseDamage.StatsValue());
                        ability2.upgradeTier++;
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility2Path3:
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability2Path2");
                        upgradeArr.Remove("DDability2Path1");
                        ability3.BaseDamage.AddFlatValue(5);
                        print("Max Dam for ability 3" + ability3.BaseDamage.StatsValue());
                        ability3.upgradeTier++;
                        break;
                    case 1:
                        ability3.BaseDamage.AddFlatValue(5);//stronger buffs or something
                        print("Max Dam " + ability3.BaseDamage.StatsValue());
                        ability3.upgradeTier++;
                        break;
                    case 2:
                        ability3.BaseDamage.AddFlatValue(5);//burn
                        print("Max Dam " + ability3.BaseDamage.StatsValue());
                        ability3.upgradeTier++;
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility3Path1:
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability3Path2");
                        upgradeArr.Remove("DDability3Path3");
                        foreach(string str in upgradeArr)
                        {
                            print(str);
                        }
                        print(upgradeArr);
                        ability1.BaseDamage.AddFlatValue(5);
                        print("Max Dam ability1 " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 1:
                        ability1.BaseDamage.AddFlatValue(5);//split
                        print("Max Dam " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 2:
                        ability1.BaseDamage.AddFlatValue(5);//burn
                        print("Max Dam " + ability1.BaseDamage.StatsValue());
                        ability1.upgradeTier++;
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility3Path2:
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability3Path1");
                        upgradeArr.Remove("DDability3Path3");
                        foreach (string str in upgradeArr)
                        {
                            print(str);
                        }
                        ability2.BaseDamage.AddFlatValue(5);
                        print("Max Dam ability 2 " + ability2.BaseDamage.StatsValue());
                        ability2.upgradeTier++;
                        break;
                    case 1:
                        ability2.BaseDamage.AddFlatValue(5);//split
                        print("Max Dam " + ability2.BaseDamage.StatsValue());
                        ability2.upgradeTier++;
                        break;
                    case 2:
                        ability2.BaseDamage.AddFlatValue(5);//burn
                        print("Max Dam " + ability2.BaseDamage.StatsValue());
                        ability2.upgradeTier++;
                        break;
                    case 3:
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.defaultDanielAbility3Path3:
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability3Path1");
                        upgradeArr.Remove("DDability3Path2");
                        foreach (string str in upgradeArr)
                        {
                            print(str);
                        }
                        ability3.BaseDamage.AddFlatValue(5);
                        print("Max Dam for ability 3" + ability3.BaseDamage.StatsValue());
                        ability3.upgradeTier++;
                        break;
                    case 1:
                        ability3.BaseDamage.AddFlatValue(5);//stronger buffs or something
                        print("Max Dam " + ability3.BaseDamage.StatsValue());
                        ability3.upgradeTier++;
                        break;
                    case 2:
                        ability3.BaseDamage.AddFlatValue(5);//burn
                        print("Max Dam " + ability3.BaseDamage.StatsValue());
                        ability3.upgradeTier++;
                        break;
                    case 3:
                        break;
                }
                break;
        }
        uIManager.HideUpgrades();
        OnLevelUp?.Invoke();
    }
}