using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BasicEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    GetPlayerPosition getPlayerPos;
    Vector2 targetPos;
    Vector2 currentPos;
    Vector2 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        getPlayerPos = GetComponent<GetPlayerPosition>();
    }

    private void Update()
    {
        currentPos = transform.position;
        targetPos = getPlayerPos.playerPos;
        //dir = new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y);
        dir = targetPos - currentPos;
        dir.Normalize();
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }
}
