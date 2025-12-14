using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AbilityManager : MonoBehaviour
{
    BaseStats bStats;
    BaseStats.Character character;
    GameObject player;
    GameObject ability1;
    GameObject ability2;
    GameObject ability3;
    TrackNeareastEnemy trackNearestEnemy;
    GameObject nearestEnemy;
    GameObject target;
    public float ability1CoolDown;
    public float ability2CoolDown;
    public float ability3CoolDown;
    bool ability1OnCoolDown=false;
    bool ability2OnCoolDown;
    bool ability3OnCoolDown;
    float ability1Timer;
    float ability2Timer;
    float ability3Timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bStats = player.GetComponent<BaseStats>();
        trackNearestEnemy = player.GetComponent<TrackNeareastEnemy>();
        character = bStats.characterSelected;
        ability1 = GameObject.FindGameObjectWithTag("Ability1");
        ability2 = GameObject.FindGameObjectWithTag("Ability2");
        ability3 = GameObject.FindGameObjectWithTag("Ability3");
        target = GameObject.FindGameObjectWithTag("Target");
        ability1CoolDown = ability1.GetComponent<AbilityStats>().Cooldown.StatsValue();
        //ability2CoolDown = ability2.GetComponent<AbilityStats>().Cooldown.StatsValue();
        //ability3CoolDown = ability3.GetComponent<AbilityStats>().Cooldown.StatsValue();
    }
    private void Update()
    {
        
        if (ability1OnCoolDown)
        {
            ability1Timer += Time.deltaTime;
        }
        if (ability1Timer >= ability1CoolDown)
        {
            ability1Timer = 0f;
            ability1OnCoolDown = false;
        }
        if (ability2OnCoolDown)
        {
            ability2Timer += Time.deltaTime;
        }
        if (ability2Timer >= ability2CoolDown)
        {
            ability2Timer = 0f;
            ability2OnCoolDown = false;
        }
        if (ability3OnCoolDown)
        {
            ability3Timer += Time.deltaTime;
        }
        if (ability3Timer >= ability3CoolDown)
        {
            ability3Timer = 0f;
            ability3OnCoolDown = false;
        }
    }
    public void Ability1()
    {
        if (ability1OnCoolDown) return;
        ability1.SetActive(true);
        ability1OnCoolDown = true;
    }

    public void Ability2()
    {
        //if (ability2OnCoolDown) return;

        //ability2.SetActive(true);
        //ability2OnCoolDown = true;
    }

    public void Ability3()
    {
        //if (ability3OnCoolDown) return;

        //ability3.SetActive(true);
        //ability3OnCoolDown = true;
    }

}

public class DefaultDanielAbilities
{

}
