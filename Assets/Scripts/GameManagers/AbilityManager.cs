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


    private void Update()
    {
        TrackAbilityCooldowns();
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu") GetReferences();
        UpgradeManager.OnLevelUp += UpdateCooldownValues;
    }

    private void OnDisable()
    {
        UpgradeManager.OnLevelUp -= UpdateCooldownValues;
    }

    public void GetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bStats = player.GetComponent<BaseStats>();
        ability1 = GameObject.FindGameObjectWithTag("Ability1");
        ability2 = GameObject.FindGameObjectWithTag("Ability2");
        ability3 = GameObject.FindGameObjectWithTag("Ability3");

        ability1CoolDown = ability1.GetComponent<AbilityStats>().Cooldown.StatsValue();
        ability2CoolDown = ability2.GetComponent<AbilityStats>().Cooldown.StatsValue();
        ability3CoolDown = ability3.GetComponent<AbilityStats>().Cooldown.StatsValue();

        ability1Timer = ability1CoolDown;
        ability2Timer = ability2CoolDown;
        ability3Timer = ability3CoolDown;
        
    }

    public void GetAbility4References()
    {
        ability4CoolDown = ability4.GetComponent<AbilityStats>().Cooldown.StatsValue();
        ability4Timer = ability4CoolDown;
    }

    void UpdateCooldownValues()
    {
        ability1CoolDown = ability1.GetComponent<AbilityStats>().Cooldown.StatsValue();
        ability2CoolDown = ability2.GetComponent<AbilityStats>().Cooldown.StatsValue();
        ability3CoolDown = ability3.GetComponent<AbilityStats>().Cooldown.StatsValue();
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
        print("A4CD: "+ability4CoolDown);
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
            ability1Charges--;
            ability1.SetActive(true);
            ability1OnCoolDown = true;
            if (ability1Timer <= 0) ability1Timer = ability1CoolDown;
        }
    }

    public void Ability2()
    {
        if (ability2Charges > 0)
        {
            if (ability2.activeInHierarchy) return;
            ability2Charges--;
            ability2.SetActive(true);
            ability2OnCoolDown = true;
            if (ability2Timer <= 0) ability2Timer = ability2CoolDown;
        }
    }

    public void Ability3()
     {
        if (ability3Charges > 0)
        {
            if (ability3.activeInHierarchy) return;
            ability3Charges--;
            ability3.SetActive(true);
            ability3OnCoolDown = true;
            if (ability3Timer <= 0) ability3Timer = ability3CoolDown;
        }
    }
    public void Ability4()
    {
        if (ability4Charges > 0)
        {
            if (ability4.activeInHierarchy) return;
            ability4Charges--;
            ability4.SetActive(true);
            ability4OnCoolDown = true;
            if (ability4Timer <= 0) ability4Timer = ability4CoolDown;
        }
    }
}