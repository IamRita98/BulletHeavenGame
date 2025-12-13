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

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bStats = player.GetComponent<BaseStats>();
        trackNearestEnemy = player.GetComponent<TrackNeareastEnemy>();
        character = bStats.characterSelected;
        ability1 = GameObject.FindGameObjectWithTag("Ability1");
        ability2 = GameObject.FindGameObjectWithTag("Ability2");
        ability3 = GameObject.FindGameObjectWithTag("Ability3");
    }

    public void Ability1()
    {
        FindNearestEnemy();
        ability1.SetActive(true);
    }

    public void Ability2()
    {
        FindNearestEnemy();
    }

    public void Ability3()
    {
        FindNearestEnemy();
    }

    void FindNearestEnemy()
    {
        nearestEnemy = trackNearestEnemy.NearestEnemy();
    }
}

public class DefaultDanielAbilities
{

}
