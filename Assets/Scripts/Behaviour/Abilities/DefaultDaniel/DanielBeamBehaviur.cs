using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DanielBeamBehaviur : MonoBehaviour
{
    public GameObject parentGO;
    public TrackNeareastEnemy trackNearestEnemy;
    public AbilityStats abilityStats;
    public bool foundNearestEnemy;
    public float timer;
    public PolygonCollider2D polygonCollider;
    public SpriteRenderer beamRenderer;
    public CombatHandler combatHandler;
    Vector3 defaultSize;
    float duration;
    float damage;

    private void Awake()
    {
        defaultSize = transform.localScale;
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        ScaleStats();
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
        foundNearestEnemy = false;
    }

    void ScaleStats()
    {
        duration = abilityStats.LifeTime.StatsValue();
        damage = abilityStats.BaseDamage.StatsValue();
    }

    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        trackNearestEnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<TrackNeareastEnemy>();
        abilityStats = GetComponent<AbilityStats>();
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
            gameObject.transform.localScale = defaultSize * abilityStats.Area.StatsValue();
            foundNearestEnemy = true;
            FindNearestEnemy();
        }

        if (timer >= duration)
        {
            timer = 0;
            transform.localScale = defaultSize;
            polygonCollider.enabled = false;
            beamRenderer.enabled = false;
            gameObject.SetActive(false);
        }
    }

    public virtual void FindNearestEnemy()
    {
        Vector2 targetPos = trackNearestEnemy.NearestEnemy().transform.position - gameObject.transform.parent.position;
        parentGO.transform.right = targetPos;
        polygonCollider.enabled = true;
        beamRenderer.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        combatHandler.HandleDamage(damage, collision.gameObject);
    }
}
