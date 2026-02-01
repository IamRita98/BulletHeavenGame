using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltBeamCircleBehaviour : MonoBehaviour
{
    AbilityStats abilityStats;
    float timer;
    public Sprite circleSprite;
    SpriteRenderer beamRenderer;
    CircleCollider2D circleAOE;
    CombatHandler combatHandler;
    float defaultRadiusSize = 5f;
    float duration;
    float damage;
    Vector3 size;

    void Awake()
    {
        GetReferences();
        SwitchOldBeamForNewCircle();
        TurnOffBeam();
    }

    void GetReferences()
    {
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        abilityStats = GetComponent<AbilityStats>();
        beamRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void SwitchOldBeamForNewCircle()
    {
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.transform.localScale = new Vector3(defaultRadiusSize, defaultRadiusSize, 0) * abilityStats.Area.StatsValue();

        circleAOE = gameObject.AddComponent<CircleCollider2D>();
        circleAOE.isTrigger = true;
        circleAOE.transform.localPosition = Vector3.zero;
    }

    void TurnOffBeam()
    {
        circleAOE.enabled = false;
        beamRenderer.enabled = false;
    }

    private void OnEnable()
    {
        circleAOE.enabled = true;
        beamRenderer.enabled = true;
        ScaleWithStats();
    }

    void ScaleWithStats()
    {
        size = new Vector3(defaultRadiusSize, defaultRadiusSize, 0) * abilityStats.Area.StatsValue();
        gameObject.transform.localScale = size;
        duration = abilityStats.LifeTime.StatsValue();
        damage = abilityStats.BaseDamage.StatsValue();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0;
            transform.localScale = new Vector3(defaultRadiusSize, defaultRadiusSize, 0);
            TurnOffBeam();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        combatHandler.HandleDamage(damage, collision.gameObject);
    }
}
