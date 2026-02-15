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
    public float ability1CoolDown;
    public float ability2CoolDown;
    public float ability3CoolDown;
    public bool ability1OnCoolDown = false;
    public bool ability2OnCoolDown = false;
    public bool ability3OnCoolDown = false;
    public float ability1Timer;
    public float ability2Timer;
    public float ability3Timer;

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

    void UpdateCooldownValues()
    {
        ability1CoolDown = ability1.GetComponent<AbilityStats>().Cooldown.StatsValue();
        ability2CoolDown = ability2.GetComponent<AbilityStats>().Cooldown.StatsValue();
        ability3CoolDown = ability3.GetComponent<AbilityStats>().Cooldown.StatsValue();
    }

    void TrackAbilityCooldowns()
    {
        //Ability 1
        if (ability1OnCoolDown) ability1Timer -= Time.deltaTime;
        if (ability1Timer <= 0) ability1OnCoolDown = false;
        //Ability 2
        if (ability2OnCoolDown) ability2Timer -= Time.deltaTime;
        if (ability2Timer <= 0) ability2OnCoolDown = false;
        //Ability 3
        if (ability3OnCoolDown) ability3Timer -= Time.deltaTime;
        if (ability3Timer <= 0) ability3OnCoolDown = false;
    }

    public void Ability1()
    {
        if (ability1OnCoolDown) return;
        ability1.SetActive(true);
        ability1OnCoolDown = true;
        ability1Timer = ability1CoolDown;
    }

    public void Ability2()
    {
        if (ability2OnCoolDown) return;
        ability2.SetActive(true);
        ability2OnCoolDown = true;
        ability2Timer = ability2CoolDown;
    }

    public void Ability3()
     {
        if (ability3OnCoolDown) return;
        ability3.SetActive(true);
        ability3OnCoolDown = true;
        ability3Timer = ability3CoolDown;
    }
}