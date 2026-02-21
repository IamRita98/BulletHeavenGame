using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BasicEnemyMovement : MonoBehaviour
{
    EnemyBaseStats eBaseStats;
    Rigidbody2D rb;
    float speed;
    GetPlayerPosition getPlayerPos;
    SpriteRenderer sRend;
    Vector2 targetPos;
    Vector2 currentPos;
    Vector2 dir;

    private void Start()
    {
        eBaseStats = GetComponent<EnemyBaseStats>();
        rb = GetComponent<Rigidbody2D>();
        getPlayerPos = GetComponent<GetPlayerPosition>();
        speed = eBaseStats.MovementSpeed.StatsValue();
        sRend = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        currentPos = transform.position;
        targetPos = getPlayerPos.playerPos;
        dir = targetPos - currentPos;
        dir.Normalize();
        speed = eBaseStats.MovementSpeed.StatsValue();
        if (targetPos.x > transform.position.x) sRend.flipX = true;
        else sRend.flipX = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }
}
