using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerPosition : MonoBehaviour
{
    GameObject player;
    public Vector2 playerPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        playerPos = player.transform.position;
    }
}
