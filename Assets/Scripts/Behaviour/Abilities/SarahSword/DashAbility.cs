using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DashAbility : MonoBehaviour
{
    Transform playerTransf;
    CombatHandler combatHandler;
    AbilityStats abilityStats;
    float duration;
    Vector2 targetPos;
    float timer = 0;
    bool gotReferences = false;
    Rigidbody2D rb;
    Vector2 dir;
    bool isDashing = false;
    bool reachedDestination = false;
    CapsuleCollider2D playerCollider;

    private void Awake()
    {
        abilityStats = gameObject.GetComponent<AbilityStats>();
        rb = gameObject.GetComponentInParent<Rigidbody2D>();
        playerCollider = gameObject.GetComponentInParent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (isDashing)
        {
            dir = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y).normalized;
            isDashing = false;
        }
        if (Vector3.Distance(transform.position, targetPos) < .25f)
        {
            rb.velocity = Vector3.zero;
            reachedDestination = true;
            playerCollider.enabled = true;
        }
        if (timer >= duration)
        {
            rb.velocity = Vector2.zero;
            gameObject.SetActive(false);
            isDashing = false;
            reachedDestination = false;
            playerCollider.enabled = true;
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (reachedDestination) return;
        playerCollider.enabled = false;
        rb.velocity = (abilityStats.ProjectileSpeed.StatsValue() * dir);
    }
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print("target pos is: " + targetPos);
        duration = abilityStats.LifeTime.StatsValue();
        isDashing = true;
        if(gotReferences) combatHandler.InvincibilityDuration(duration);
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
    }
    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        gotReferences = true;
        gameObject.SetActive(false);
        
    }
}
