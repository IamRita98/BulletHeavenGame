using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class AbilityManager : MonoBehaviour
{
    BaseStats bStats;
    GameObject player;
    GameObject ability1;
    GameObject ability2;
    GameObject ability3;
    public GameObject ability4;
    public float ability1CoolDown;
    public float ability2CoolDown;
    public float ability3CoolDown;
    public float ability4CoolDown = 5;
    public bool ability1OnCoolDown = false;
    public bool ability2OnCoolDown = false;
    public bool ability3OnCoolDown = false;
    public bool ability4OnCoolDown = false;
    public float ability1Timer;
    public float ability2Timer;
    public float ability3Timer;
    public float ability4Timer;
    public int ability1Charges = 1;
    public int ability1MaxCharges = 1;
    public int ability2Charges = 1;
    public int ability2MaxCharges=1;
    public int ability3Charges = 1;
    public int ability3MaxCharges = 1;
    public int ability4Charges = 1;
    public int ability4MaxCharges = 1;
    public bool cooldownT3 = false;
    public bool isAbilitiesInitialized = false;
    AbilityStats[] abilities;
    private void Update()
    {
        if (player == null)
        {
            isAbilitiesInitialized = false;
            GetReferences();
            if(player!=null) isAbilitiesInitialized = true;

        }
        if(player!=null) TrackAbilityCooldowns();

    }

    private void OnEnable()
    {
        //if (SceneManager.GetActiveScene().name != "MainMenu") GetReferences();
        if (SceneManager.GetActiveScene().name == "MainMenu") isAbilitiesInitialized = false;
        UpgradeManager.OnLevelUp += UpdateCooldownValues;
        CombatHandler.OnPlayerDeath += ResetCooldowns;
    }

    private void OnDisable()
    {
        UpgradeManager.OnLevelUp -= UpdateCooldownValues;
        CombatHandler.OnPlayerDeath -= ResetCooldowns;
    }
    public void ResetCooldowns()
    {
        ability1OnCoolDown = false;
        ability2OnCoolDown = false;
        ability3OnCoolDown = false;
        ability1Timer = ability1CoolDown;
        ability2Timer = ability2CoolDown;
        ability3Timer = ability3CoolDown;
        ability1Charges = ability1MaxCharges;
        ability2Charges = ability2MaxCharges;
        ability3Charges = ability3MaxCharges;
        if (ability4)
        {
            ability4OnCoolDown = false;
            ability4Timer = ability4CoolDown;
            ability4Charges = ability4MaxCharges;
        }
    }
    public void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        abilities = player.transform.GetComponentsInChildren<AbilityStats>(true);
        bStats = player.GetComponent<BaseStats>();
        if (abilities.Length > 0)
        {
            ability1 = abilities[0].gameObject;
            ability1CoolDown = ability1.GetComponent<AbilityStats>().Cooldown.StatsValue();
        }
        if (abilities.Length > 1)
        {
            ability2 = abilities[1].gameObject;
            ability2CoolDown = ability2.GetComponent<AbilityStats>().Cooldown.StatsValue();
        }
        if (abilities.Length > 2)
        {
            ability3 = abilities[2].gameObject;
            ability3CoolDown = ability3.GetComponent<AbilityStats>().Cooldown.StatsValue();
        }
        ability1Timer = ability1CoolDown;
        ability2Timer = ability2CoolDown;
        ability3Timer = ability3CoolDown;
        //will implement ability4 later
    }

    public void GetAbility4References()
    {
        ability4CoolDown = ability4.GetComponent<AbilityStats>().Cooldown.StatsValue();
        ability4Timer = ability4CoolDown;
    }

    void UpdateCooldownValues()
    {
        if(ability1 !=null) ability1CoolDown = ability1.GetComponent<AbilityStats>().Cooldown.StatsValue();
        if (ability2 != null) ability2CoolDown = ability2.GetComponent<AbilityStats>().Cooldown.StatsValue();
        if (ability3 != null) ability3CoolDown = ability3.GetComponent<AbilityStats>().Cooldown.StatsValue();
        if(ability4!=null) ability4CoolDown = ability4.GetComponent<AbilityStats>().Cooldown.StatsValue();
    }

    void TrackAbilityCooldowns()
    {
        //Ability 1
        if (ability1OnCoolDown) ability1Timer -= Time.deltaTime;
        if (ability1Timer <= 0)
        {
            ability1OnCoolDown = false;
            if(ability1MaxCharges>ability1Charges) ability1Charges++;
        }
        //Ability 2
        if (ability2OnCoolDown) ability2Timer -= Time.deltaTime;
        if (ability2Timer <= 0)
        {
            ability2OnCoolDown = false;
            if (ability2MaxCharges > ability2Charges) ability2Charges++;
        }
        //Ability 3
        if (ability3OnCoolDown) ability3Timer -= Time.deltaTime;
        if (ability3Timer <= 0)
        {
            ability3OnCoolDown = false;
            if (ability3MaxCharges > ability3Charges) ability3Charges++;
        }
        //Ability 4
        if (ability4OnCoolDown) ability4Timer -= Time.deltaTime;
        if (ability4Timer <= 0)
        {
            ability4OnCoolDown = false;
            if (ability4MaxCharges > ability4Charges) ability4Charges++;
        }
    }

    public void Ability1()
    {
        if (ability1Charges > 0)
        {
            if (ability1.activeInHierarchy) return;
            if (cooldownT3 && RollToResetAbility())
            {
                ability1.SetActive(true);
                return;
            }
            ability1Charges--;
            ability1OnCoolDown = true;
            ability1.SetActive(true);
            ability1Timer = ability1CoolDown;
        }
    }

    public void Ability2()
    {
        if (ability2Charges > 0)
        {
            if (ability2.activeInHierarchy) return;
            if (cooldownT3 && RollToResetAbility())
            {
                ability2.SetActive(true);
                return;
            }
            ability2Charges--;
            ability2.SetActive(true);
            ability2OnCoolDown = true;
            ability2Timer = ability2CoolDown;
        }
    }

    public void Ability3()
     {
        if (ability3Charges > 0)
        {
            if (ability3.activeInHierarchy) return;
            if (cooldownT3 && RollToResetAbility())
            {
                ability3.SetActive(true);
                return;
            }
            ability3Charges--;
            ability3.SetActive(true);
            ability3OnCoolDown = true;
            ability3Timer = ability3CoolDown;
        }
    }
    public void Ability4()
    {
        if (ability4Charges > 0)
        {
            if (ability4.activeInHierarchy) return;
            if (cooldownT3 && RollToResetAbility())
            {
                ability4.SetActive(true);
                return;
            }
            ability4Charges--;
            ability4.SetActive(true);
            ability4OnCoolDown = true;
            ability4Timer = ability4CoolDown;
        }
    }
    float wins;
    bool RollToResetAbility()
    {
        
        float cdResetThreshold = 90;
        float tmpRoll = Random.Range(1, 101);
        print("Rolled: "+tmpRoll);
        if (tmpRoll > cdResetThreshold)
        {
            wins++;
            if(wins > 1)
            {
                print ("I can't stop winning!");
            }
            print("DINGDINGDINGDING YOU WIN");
            return true;
        }
        wins = 0;
        return false;
    }
}