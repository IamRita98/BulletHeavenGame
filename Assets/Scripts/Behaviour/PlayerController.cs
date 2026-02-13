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
    bool spriteIsFlipped = false;
    SpriteRenderer sRend;
    Animator anim;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        bStats = gameObject.GetComponent<BaseStats>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = bStats.MovementSpeed.StatsValue();
        abilityManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<AbilityManager>();
        gameStateManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<GameStateManager>();
        sRend = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        anim = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<Animator>();
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
        if (gameStateManager.gameIsPaused) return;
        Movement();
        CheckForSpriteFlip();
        Animations();
        if (Input.GetKeyDown(KeyCode.Mouse0)) abilityManager.Ability1();
        if (Input.GetKeyDown(KeyCode.Mouse1)) abilityManager.Ability2();
        if (Input.GetKeyDown(KeyCode.Space)) abilityManager.Ability3();
    }

    void Movement()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        dir.Normalize();
    }

    void CheckForSpriteFlip()
    {
        if (!spriteIsFlipped && dir.x < 0)
        {
            sRend.flipX = true;
            spriteIsFlipped = true;
        }
        if(spriteIsFlipped && dir.x > 0)
        {
            sRend.flipX = false;
            spriteIsFlipped = false;
        }
    }

    void Animations()
    {
        if (dir.x != 0 || dir.y != 0) anim.SetBool("isMoving", true);
        else anim.SetBool("isMoving", false);
    }

    void KillPlayer()
    {
        //Prob check for revives or something idk yet
    }
}
