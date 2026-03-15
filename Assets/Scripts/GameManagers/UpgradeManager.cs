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

    AbilityManager abilityManager;
    public GameObject genDash;
    ObjectPooling bulletOPool;
    ObjectPooling petProjPool;
    ObjectPooling enemyPool;
    public SpritesReferenceSO spriteReferences;
    CombatHandler cHandler;
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
    List<BaseStats.Character> playerCharactersWithDashes = new List<BaseStats.Character> { BaseStats.Character.SarahSword };
    
    List<string> upgradeArr = new List<string> { "globalDam", "fireRate", "health", "projectile", "area", "duration", "speed", "cooldown" };
    List<string> defaultDaniel = new List<string> { "DDautoAttack", "DDability1Path1", "DDability1Path2", "DDability1Path3", "DDability2Path1", "DDability2Path2", "DDability2Path3", "DDability3Path1", 
                                                    "DDability3Path2" , "DDability3Path3" };
    GameObject playerCharacter;
    AbilityStats[] abilities;

    UIManager uIManager;
    public enum UpgradeTypes
    {   //global upgrades
        weapFireRate,
        duration,
        area,
        health,
        projectiles,
        globalDam,
        speed,
        cooldown,

        //character specific upgrades
        //defaultDaniel
        defaultDanielAutoAttacks,
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

    public bool durationT3Taken = false;

    private void Awake()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        cHandler = gameObject.GetComponent<CombatHandler>();
        bulletOPool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ObjectPooling>();
        petProjPool = GameObject.FindGameObjectWithTag("PetProjPool").GetComponent<ObjectPooling>();
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPooling>();
        abilityManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<AbilityManager>();
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
                UpgradeTypes.globalDam,
                new UpgradeInfo{
                    GetTier = () => playerBStats.globalDamT,
                    descriptions = new Dictionary<int, string>
                    {
                        {0,"+15% to Global Damage"},
                        {1," +40% Global Damage, \n +10% Area, \n -20% Attackrate" },
                        {2,"Enemies have a 25% chance of exploding for 30% of the damage of the killing blow" },
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
                        {1,"+40% Firerate, -10% Weapon Damage" },
                        {2, "+1 Projectile,\n +10% Firerate, each subsequent hit on an enemy adds +1 damage(max 35 stacks)" }
                    }
                }
            },
            {
                UpgradeTypes.duration,
                new UpgradeInfo
                {
                    GetTier = () => baseWeaponStats.lifeTimeUpgT,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"+15% Duration" },
                        {1,"+25% Duration,\n +5% damage,\n & +10 HP" },
                        {2, "-20% Projectile speed,\n all projectiles now have a damaging AoE around them as they travel" }
                    }
                }
            },
            {
                UpgradeTypes.area,
                new UpgradeInfo
                {
                    GetTier = () => playerBStats.areaUpgT,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"+10% Area on attacks and abilities" },
                        {1,"+20% Area on attacks and abilities" },
                        {2,"70% Area on attacks and abilities \n But, deal 15% less damage and attack 10% slower" },
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
                        {0,"+15 HP" },
                        {1,"+60% HP, \n Gain +2 hp regen" },
                        {2,"Every 10 minutes gain 1 revive. \n On death kill all non-boss enemies on screen. \n +1 hp regen" },
                    }
                }
            },
            {
                UpgradeTypes.speed,
                new UpgradeInfo
                {
                    GetTier = () => playerBStats.movementSpUpgT,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"+15% movement speed" },
                        {1,"Gain a short invlun dash on a 10s cd(Gain an extra dash charge if you already have a dash)" },
                        {2,"Gain 100% increased movement speed but enemies gain 50% movement speed" },
                    }
                }
            },
            {
                UpgradeTypes.cooldown,
                new UpgradeInfo
                {
                    GetTier = () => playerBStats.cooldownT,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"10% cooldown reduction to abilities" },
                        {1,"20% cooldown reduction to abilities, \n -10% attackrate" },
                        {2, "10% chance on ability use for the ability to not go on CD" }
                    }
                }
            },
            {
                UpgradeTypes.defaultDanielAutoAttacks,
                new UpgradeInfo
                {
                    GetTier = () => ability1.upgradeTier,
                    descriptions= new Dictionary<int, string>
                    {
                        {0,"AutoAttack1"},
                        {1,"AutoAttack2" },
                        {2,"AutoAttack3"}
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
                        {2,"etc..." }
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
                        {2,"etc..." }
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
                        {0,"Bullets from ring now chain to 1 enemy instead of piercing"},
                        {1,"Number of Chains now scales with pierce" },
                        {2,"Bullets that chain also split into smaller bullets" }
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
                        {0,"Buff area now moves with you"},
                        {1,"Buff lasts 50% longer" },
                        {2,"Buff occasionally shoots a wave at nearby enemies" }
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
            case "area":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.area], upgradeButton, UpgradeTypes.area);
                break;
            case "duration":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.duration], upgradeButton, UpgradeTypes.duration);
                break;
            case "speed":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.speed], upgradeButton, UpgradeTypes.speed);
                break;
            case "cooldown":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.cooldown], upgradeButton, UpgradeTypes.cooldown);
                break;
            case "DDautoAttack":
                uIManager.DisplayUpgrade(upgrades[UpgradeTypes.defaultDanielAutoAttacks], upgradeButton, UpgradeTypes.defaultDanielAutoAttacks);
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
            /// <summary>
            /// Global upgrades
            /// </summary>
            case UpgradeManager.UpgradeTypes.globalDam: //Increment both Weap dam & all ability dam, then increment the tier stat in base stats
                switch (tier)
                {
                    case 0:
                        foreach (var ability in abilities)
                        {
                            ability.BaseDamage.AddMultiValue(1.15f);
                        }
                        baseWeaponStats.BaseDamage.AddMultiValue(1.15f);
                        playerBStats.globalDamT++;
                        break;
                    case 1:
                        foreach (var ability in abilities)
                        {
                            ability.BaseDamage.AddMultiValue(1.4f);
                        }
                        baseWeaponStats.BaseDamage.AddMultiValue(1.4f);
                        baseWeaponStats.AttackRate.AddFlatMultiValue(-.2f);
                        playerBStats.globalDamT++;
                        break;
                    case 2:
                        upgradeArr.Remove("globalDam");
                        cHandler.shouldExplode = true;
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.weapFireRate:
                switch (tier)
                {
                    case 0:
                        baseWeaponStats.AttackRate.AddMultiValue(.85f);
                        baseWeaponStats.attackRUpgT++;
                        break;
                    case 1:
                        baseWeaponStats.AttackRate.AddMultiValue(.60f);
                        baseWeaponStats.BaseDamage.AddFlatMultiValue(-.1f);
                        baseWeaponStats.attackRUpgT++;
                        break;
                    case 2:
                        baseWeaponStats.Projectiles.AddFlatValue(1);
                        baseWeaponStats.AttackRate.AddMultiValue(0.9f);
                        cHandler.fireRateTier3 = true;
                        baseWeaponStats.attackRUpgT++;
                        upgradeArr.Remove("fireRate");
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.duration:
                switch (tier)
                {
                    case 0:
                        baseWeaponStats.LifeTime.AddFlatMultiValue(.15f);
                        foreach(AbilityStats abilityStat in abilities)
                        {
                            abilityStat.LifeTime.AddFlatMultiValue(.15f);
                        }
                        baseWeaponStats.lifeTimeUpgT++;
                        break;
                    case 1:
                        baseWeaponStats.LifeTime.AddFlatMultiValue(.25f);
                        baseWeaponStats.BaseDamage.AddMultiValue(1.05f);
                        playerBStats.MaxHealth.AddFlatValue(10);
                        playerBStats.Health.AddFlatValue(10);
                        foreach (AbilityStats abilityStat in abilities)
                        {
                            abilityStat.LifeTime.AddFlatMultiValue(.25f);
                            abilityStat.BaseDamage.AddMultiValue(1.05f);
                        }
                        baseWeaponStats.lifeTimeUpgT++;
                        break;
                    case 2:
                        baseWeaponStats.ProjectileSpeed.AddFlatMultiValue(-.2f);
                        durationT3Taken = true;
                        foreach (AbilityStats abilityStat in abilities) abilityStat.ProjectileSpeed.AddFlatMultiValue(-.2f);
                        baseWeaponStats.lifeTimeUpgT++;
                        upgradeArr.Remove("duration");
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.area:
                switch (tier)
                {
                    case 0:
                        baseWeaponStats.WeapArea.AddMultiValue(1.10f);
                        foreach (AbilityStats aStats in abilities)
                        {
                            aStats.Area.AddMultiValue(1.10f);
                        }
                        playerBStats.areaUpgT++;
                        break;
                    case 1:
                        baseWeaponStats.WeapArea.AddMultiValue(1.20f);
                        foreach (AbilityStats aStats in abilities)
                        {
                            aStats.Area.AddMultiValue(1.20f);
                        }
                        playerBStats.areaUpgT++;
                        break;
                    case 2:
                        baseWeaponStats.WeapArea.AddMultiValue(1.69f);
                        baseWeaponStats.BaseDamage.AddMultiValue(.85f);
                        baseWeaponStats.AttackRate.AddMultiValue(.90f);
                        foreach (AbilityStats aStats in abilities)
                        {
                            aStats.Area.AddMultiValue(1.69f);
                            aStats.BaseDamage.AddMultiValue(.85f);
                        }
                        upgradeArr.Remove("area");
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
            case UpgradeManager.UpgradeTypes.health:
                HealthRegen hRegen = playerCharacter.GetComponent<HealthRegen>();
                switch (tier)
                {
                    case 0:
                        playerBStats.Health.AddFlatValue(15);
                        playerBStats.MaxHealth.AddFlatValue(15);
                        playerBStats.healthUpgT++;
                        break;
                    case 1:
                        playerBStats.Health.AddMultiValue(1.60f);
                        playerBStats.MaxHealth.AddMultiValue(1.60f);
                        playerBStats.healthUpgT++;
                        hRegen.regenValue += 2;
                        break;
                    case 2:
                        cHandler.revives++;
                        cHandler.hasRevives = true;
                        playerBStats.healthUpgT++;
                        upgradeArr.Remove("health");
                        hRegen.regenValue += 1;
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.speed:
                switch (tier)
                {
                    case 0:
                        playerBStats.MovementSpeed.AddMultiValue(1.15f);
                        print("Speed: " + playerBStats.MovementSpeed.StatsValue());
                        playerBStats.movementSpUpgT++;
                        break;
                    case 1:
                        playerBStats.movementSpUpgT++;
                        GameObject tempDashGO=Instantiate(genDash,playerCharacter.transform);
                        //tempDashGO.transform.SetParent(playerCharacter.transform);
                        tempDashGO.transform.localPosition = Vector3.zero;
                        if (playerCharactersWithDashes.Contains(playerBStats.characterSelected)) abilityManager.ability2Charges++;
                        else abilityManager.ability4 = tempDashGO;
                        uIManager.ability4CD.gameObject.SetActive(true);
                        abilityManager.GetAbility4References();
                        //abilityManager.ability4CoolDown = tempDashGO.GetComponent<AbilityStats>().Cooldown.StatsValue();
                        tempDashGO.SetActive(false);
                        break;
                    case 2:
                        playerBStats.MovementSpeed.AddMultiValue(2f);
                        print("Speed: " + playerBStats.MovementSpeed.StatsValue());
                        foreach (GameObject enemy in enemyPool.activePool)
                        {
                            enemy.GetComponent<BaseStats>().MovementSpeed.AddMultiValue(1.5f);
                        }
                        foreach (GameObject enemy in enemyPool.objectPool)
                        {
                            enemy.GetComponent<BaseStats>().MovementSpeed.AddMultiValue(1.5f);
                        }
                        upgradeArr.Remove("speed");
                        break;
                }
                break;
            case UpgradeManager.UpgradeTypes.cooldown:
                switch (tier)
                {
                    case 0:
                        foreach(AbilityStats aStats in abilities)
                        {
                            aStats.Cooldown.AddMultiValue(.9f);
                        }
                        playerBStats.cooldownT++;
                        break;
                    case 1:
                        foreach (AbilityStats aStats in abilities)
                        {
                            aStats.Cooldown.AddMultiValue(.8f);
                        }
                        baseWeaponStats.AttackRate.AddMultiValue(.9f);
                        playerBStats.cooldownT++;
                        break;
                    case 2:
                        abilityManager.cooldownT3 = true;
                        upgradeArr.Remove("cooldown");
                        break;
                }
                break;
            /// <summary>
            /// DefaultDanielUpgrades
            /// </summary>
            case UpgradeManager.UpgradeTypes.defaultDanielAutoAttacks:
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
            // DDability 1
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
            //DD ability 2
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
                BulletRingBehaviour ability2Behav = ability2GO.GetComponent<BulletRingBehaviour>();
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability2Path2");
                        upgradeArr.Remove("DDability2Path1");
                        ability2Behav.path2Tier1 = true;
                        ability2.upgradeTier++;
                        break;
                    case 1:
                        ability2Behav.path2Tier2 = true;
                        ability2.upgradeTier++;
                        break;
                    case 2:
                        ability2.upgradeTier++;
                        ability2Behav.path2Tier3=true;
                        upgradeArr.Remove("DDability2Path3");
                        break;
                    case 3:
                        break;
                }
                break;
            //DD ability 3
            case UpgradeManager.UpgradeTypes.defaultDanielAbility3Path1:
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability3Path2");
                        upgradeArr.Remove("DDability3Path3");
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
                DefaultDanielAbility3Behaviour ability3Behav = ability3GO.GetComponent<DefaultDanielAbility3Behaviour>();
                PetFiringGeneric supportFire = ability3GO.GetComponent<PetFiringGeneric>();
                switch (tier)
                {
                    case 0:
                        upgradeArr.Remove("DDability3Path1");
                        upgradeArr.Remove("DDability3Path2");
                        if (ability3Behav.isActive)
                        {
                            ability3GO.transform.parent = playerCharacter.transform;
                            ability3GO.transform.localPosition= Vector3.zero;
                        }
                        ability3Behav.path3Tier1 = true;
                        ability3.upgradeTier++;
                        break;
                    case 1:
                        ability3.upgradeTier++;
                        ability3.LifeTime.AddMultiValue(1.5f);
                        break;
                    case 2:
                        supportFire.activate = true;
                        ability3Behav.path3Tier3 = true;
                        supportFire.isUpgraded=true;
                        upgradeArr.Remove("DDability3Path3");
                        ability3.upgradeTier++;
                        break;
                }
                break;
                /// <summary>
                /// SarahSword Upgrades (to come)
                /// </summary>
        }
        uIManager.HideUpgrades();
        OnLevelUp?.Invoke();
    }
}