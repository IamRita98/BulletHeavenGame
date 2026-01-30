using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    AbilityManager abilityManager;
    private void Awake()
    {
        abilityManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<AbilityManager>();
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
    }

    void ResetCooldowns()
    {
        abilityManager.ability1Timer = 0;
        abilityManager.ability2Timer = 0;
        abilityManager.ability3Timer = 0;
    }

    void ResetToMaxHP()
    {

    }

    void LevelUp()
    {

    }
}
