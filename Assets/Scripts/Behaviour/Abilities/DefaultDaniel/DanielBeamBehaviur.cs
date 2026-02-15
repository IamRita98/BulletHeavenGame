using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DanielBeamBehaviur : MonoBehaviour
{
    ObjectPooling xpPool;
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
    public bool path2Tier2 = false;
    float flatYScalingOfBigBeam = 1f;
    public bool path2Tier3 = false;
    float burnDamage = 1f;
    float burnDuration = 3f;
    float beamDefaultXpos;
    

    private void Awake()
    {
        defaultSize = transform.localScale;
        beamDefaultXpos = transform.localPosition.x;
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
        transform.localPosition = new Vector3(beamDefaultXpos * abilityStats.Area.StatsValue(), transform.localPosition.y, transform.localPosition.z);
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
        xpPool = GameObject.FindGameObjectWithTag("XpPool").GetComponent<ObjectPooling>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!foundNearestEnemy)
        {
            Vector3 beamScale = defaultSize * abilityStats.Area.StatsValue();
            if (path2Tier2)
            {
                beamScale = new Vector3(beamScale.x, beamScale.y + flatYScalingOfBigBeam, beamScale.z);
            }
            gameObject.transform.localScale = beamScale;
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
        if (path2Tier3)
        {
            if (collision.gameObject.activeInHierarchy)
            {
                DamageOverTime burn = collision.GetComponent<DamageOverTime>();
                burn.ApplyDamageOverTimeEffect(burnDamage, burnDuration, DamageOverTime.DoTType.burn);
            }
            else if(!collision.gameObject.activeInHierarchy)
            {
                GameObject gemToSpawn = xpPool.objectPool[0];
                gemToSpawn.SetActive(true);
                xpPool.activePool.Add(gemToSpawn);
                xpPool.objectPool.RemoveAt(0);
                gemToSpawn.transform.position = collision.transform.position;
                gemToSpawn.transform.rotation = new Quaternion(0,0,90,0);

            }
        }
    }
}
