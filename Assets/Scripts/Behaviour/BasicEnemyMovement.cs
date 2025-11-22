using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BasicEnemyMovement : MonoBehaviour
{
    EnemyBaseStats eBaseStats;
    Rigidbody2D rb;
    [SerializeField] float speed;
    GetPlayerPosition getPlayerPos;
    Vector2 targetPos;
    Vector2 currentPos;
    Vector2 dir;

    private void Start()
    {
        eBaseStats = GetComponent<EnemyBaseStats>();
        rb = GetComponent<Rigidbody2D>();
        getPlayerPos = GetComponent<GetPlayerPosition>();
        speed = eBaseStats.MovementSpeed.StatsValue();
    }

    private void Update()
    {
        currentPos = transform.position;
        targetPos = getPlayerPos.playerPos;
        dir = targetPos - currentPos;
        dir.Normalize();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }
}
