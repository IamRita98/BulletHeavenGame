using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 dir;
    Rigidbody2D rb;
    BaseStats bStats;
    float moveSpeed;

    private void Start()
    {
        bStats = gameObject.GetComponent<BaseStats>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = bStats.MovementSpeed.StatsValue();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * moveSpeed;
    }

    private void Update()
    {
        Movement();

    }

    void Movement()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        dir.Normalize();
    }
}
