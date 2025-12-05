using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField]float XPThreshold = 10;//base value
    BaseStats playerStats;
    ObjectPooling xpPool;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        xpPool = GameObject.FindGameObjectWithTag("XpPool").GetComponent<ObjectPooling>();
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
    }

    void ScalingThreshold()
    {
        XPThreshold *= 2;
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
