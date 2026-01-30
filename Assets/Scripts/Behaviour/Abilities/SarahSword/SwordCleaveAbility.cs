using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwordCleaveAbility : DanielBeamBehaviur
{
    

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
        foundNearestEnemy = false;
    }

    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;

        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        trackNearestEnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<TrackNeareastEnemy>();
        abilityStats = gameObject.GetComponent<AbilityStats>();
        beamRenderer = gameObject.GetComponent<SpriteRenderer>();
        parentGO = gameObject.transform.parent.gameObject;
        polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        gameObject.SetActive(false);
        polygonCollider.enabled = false;
        beamRenderer.enabled = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!foundNearestEnemy)
        {
            foundNearestEnemy = true;
            TargetMousePosition();
            //FindNearestEnemy();
        }

        if (timer >= abilityStats.LifeTime.StatsValue())
        {
            timer = 0;
            polygonCollider.enabled = false;
            beamRenderer.enabled = false;
            gameObject.SetActive(false);
        }
    }

    void TargetMousePosition()
    {
        Vector2 targetPos =  Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.parent.position;
        parentGO.transform.up = targetPos;
        polygonCollider.enabled = true;
        beamRenderer.enabled = true;
    }

    /*public override void FindNearestEnemy()
    {
        Vector2 targetPos = trackNearestEnemy.NearestEnemy().transform.position - gameObject.transform.parent.position;
        parentGO.transform.up = targetPos;
        polygonCollider.enabled = true;
        beamRenderer.enabled = true;
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        combatHandler.HandleDamage(abilityStats.BaseDamage.StatsValue(), collision.gameObject);
    }
}
