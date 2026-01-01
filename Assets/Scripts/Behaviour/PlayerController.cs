using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 dir;
    Rigidbody2D rb;
    BaseStats bStats;
    float moveSpeed;
    AbilityManager abilityManager;
    GameStateManager gameStateManager;

    private void Start()
    {
        bStats = gameObject.GetComponent<BaseStats>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = bStats.MovementSpeed.StatsValue();
        abilityManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AbilityManager>();
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
    }

    private void OnEnable()
    {
        CombatHandler.OnPlayerDeath += KillPlayer;
    }
    private void OnDisable()
    {
        CombatHandler.OnPlayerDeath -= KillPlayer;
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * moveSpeed;
    }

    private void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameStateManager.gameIsPaused) abilityManager.Ability1();
        if (Input.GetKeyDown(KeyCode.Mouse1) && !gameStateManager.gameIsPaused) abilityManager.Ability2();
        if (Input.GetKeyDown(KeyCode.Space) && !gameStateManager.gameIsPaused) abilityManager.Ability3();
    }

    void Movement()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        dir.Normalize();
    }

    void KillPlayer()
    {
        //Prob check for revives or something idk yet
    }
}
