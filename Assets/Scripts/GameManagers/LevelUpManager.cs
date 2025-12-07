using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField]float XPThreshold = 10;//base value
    BaseStats playerStats;
    ObjectPooling xpPool;
    BaseWeaponStats bws;
    UpgradeManager um;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        xpPool = GameObject.FindGameObjectWithTag("XpPool").GetComponent<ObjectPooling>();
        um = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeManager>();
    }

    private void OnEnable()
    {
        PickupBehavior.OnXPPickup += HandleXPPickup;
        CombatHandler.OnEnemyDeath += DropXpOnEnemyDeath;
    }

    private void OnDisable()
    {
        PickupBehavior.OnXPPickup -= HandleXPPickup;
        CombatHandler.OnEnemyDeath -= DropXpOnEnemyDeath;
    }

    void HandleXPPickup(GameObject XpGO)
    {
        float XpValue = XpGO.GetComponent<XpPickupStats>().xpValue;
        playerStats.XP.AddFlatValue(XpValue);
        CheckIfLeveled();

        xpPool.activePool.Remove(XpGO);
        xpPool.objectPool.Add(XpGO);
        XpGO.SetActive(false);
    }

    void CheckIfLeveled()
    {
        print(playerStats.XP.StatsValue() + "/" + XPThreshold + "XP");
        if (playerStats.XP.StatsValue() >= XPThreshold)
        {
            print("LevelUp");
            LevelUp();
        }
    }

    void LevelUp()
    {
        //other things
        playerStats.XP.AddFlatValue(-XPThreshold);//subtract level up xp from current avoid overwriting
        ScalingThreshold();
        switch (playerStats.characterSelected)
        {
            case BaseStats.Character.DefaultDaniel:
                playerStats.MaxHealth.AddFlatValue(3);
                playerStats.Health.AddFlatValue(3);
                //ability damage stat increase here
                //ability cdr stat increase here
                bws.BaseDamage.AddMultiValue(.01f);
                break;
            case BaseStats.Character.SarahSword:
                playerStats.MaxHealth.AddFlatValue(4);
                playerStats.Health.AddFlatValue(4);
                //ability damage stat increase here
                bws.BaseDamage.AddMultiValue(.02f);
                break;
        }
        um.RollUpgrades();
    }

    void ScalingThreshold()
    {
        XPThreshold *= 2;
        //will add more to xp scaling
    }

    void DropXpOnEnemyDeath(GameObject gObject)
    {
        GameObject xpToSpawn = xpPool.objectPool[0];
        xpToSpawn.SetActive(true);
        xpPool.objectPool.Remove(xpToSpawn);
        xpPool.activePool.Add(xpToSpawn);
        xpToSpawn.transform.position = gObject.transform.position;
    }
}
