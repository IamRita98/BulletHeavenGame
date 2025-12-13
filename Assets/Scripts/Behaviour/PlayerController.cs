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

    private void Start()
    {
        bStats = gameObject.GetComponent<BaseStats>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = bStats.MovementSpeed.StatsValue();
        abilityManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AbilityManager>();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * moveSpeed;
    }

    private void Update()
    {
        Movement();

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
}
