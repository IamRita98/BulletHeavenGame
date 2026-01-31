using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltBeamCircleBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject parentGO;
    AbilityStats abilityStats;
    float timer;
    public Sprite circleSprite;
    SpriteRenderer beamRenderer;
    PolygonCollider2D beamAOE;
    CircleCollider2D circleAOE;
    //string circlePath = "Assets\\Sprites\\Circle.png";
    CombatHandler combatHandler;
    void Awake()
    {
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        abilityStats = GetComponent<AbilityStats>();
        //destroying old sprite renderer/polygon collider
        beamAOE= gameObject.GetComponent<PolygonCollider2D>();
        Destroy(beamAOE);

        //adding new components
        gameObject.transform.localScale = new Vector3(7, 7, 0);
        //beamRenderer.sprite = Resources.Load<Sprite>(circlePath);
        circleAOE = gameObject.AddComponent<CircleCollider2D>();
        //beamRenderer.color = new Vector4(9, 65, 204, 255); //color rgb values
        circleAOE.isTrigger = true;
        circleAOE.transform.position = Vector3.zero;


        parentGO = gameObject.transform.parent.gameObject;
        gameObject.SetActive(false);
        circleAOE.enabled = false;
        beamRenderer.enabled = false;
    }
    private void OnEnable()
    {
        circleAOE.enabled = true;
        beamRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= abilityStats.LifeTime.StatsValue())
        {
            timer = 0;
            circleAOE.enabled = false;
            beamRenderer.enabled = false;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        combatHandler.HandleDamage(abilityStats.BaseDamage.StatsValue(), collision.gameObject);
    }
}
