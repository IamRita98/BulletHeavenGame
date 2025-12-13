using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanielBeamBehaviur : MonoBehaviour
{
    GameObject parentGO;
    TrackNeareastEnemy trackNearestEnemy;
    AbilityStats abilityStats;
    bool foundNearestEnemy;
    float timer;

    private void Start()
    {
        trackNearestEnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<TrackNeareastEnemy>();
        parentGO = gameObject.transform.parent.gameObject;
        abilityStats = GetComponent<AbilityStats>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        foundNearestEnemy = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!foundNearestEnemy) 
        {
            foundNearestEnemy = true;
            FindNearestEnemy();
        } 

        if(timer >= abilityStats.LifeTime.StatsValue())
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }

    private void FindNearestEnemy()
    {
        Vector2 targetPos = trackNearestEnemy.NearestEnemy().transform.position - gameObject.transform.parent.position;
        parentGO.transform.right = targetPos;
        print(parentGO);
        print(trackNearestEnemy.NearestEnemy());
    }
}
