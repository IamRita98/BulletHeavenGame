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
    BoxCollider2D beamBox;
    SpriteRenderer beamRenderer;

    public static event System.Action<float, GameObject> OnAttackHit;
    private void Start()
    {
        trackNearestEnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<TrackNeareastEnemy>();
        parentGO = gameObject.transform.parent.gameObject;
        abilityStats = GetComponent<AbilityStats>();
        //gameObject.SetActive(false);
        beamBox = gameObject.GetComponent<BoxCollider2D>();
        beamRenderer = gameObject.GetComponent<SpriteRenderer>();
        beamBox.enabled = false;
        beamRenderer.enabled = false;
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

        if (timer >= abilityStats.LifeTime.StatsValue())
        {
            timer = 0;
            beamBox.enabled = false;
            beamRenderer.enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void FindNearestEnemy()
    {
        Vector2 targetPos = trackNearestEnemy.NearestEnemy().transform.position - gameObject.transform.parent.position;
        parentGO.transform.right = targetPos;
        beamBox.enabled = true;
        beamRenderer.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        OnAttackHit?.Invoke(abilityStats.BaseDamage.StatsValue(), collision.gameObject);
    }
}
