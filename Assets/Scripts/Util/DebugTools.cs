using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    AbilityManager abilityManager;
    BaseStats baseStats;
    LevelUpManager levelUpManager;
    private void Awake()
    {
        abilityManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<AbilityManager>();
        baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityStats>();
        levelUpManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelUpManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ResetCooldowns();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ResetToMaxHP();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LevelUp();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SlowDownGame();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SpeedUpGame();
        }
    }

    void ResetCooldowns()
    {
        abilityManager.ability1Timer = 0;
        abilityManager.ability2Timer = 0;
        abilityManager.ability3Timer = 0;
    }
    void SlowDownGame()
    {
        Time.timeScale = .2f;
    }
    void SpeedUpGame()
    {
        Time.timeScale = 1f;
    }
    void ResetToMaxHP()
    {
        baseStats.Health.StatOverwrite(baseStats.MaxHealth.StatsValue());
    }

    void LevelUp()
    {
        levelUpManager.LevelUp();
    }
}
