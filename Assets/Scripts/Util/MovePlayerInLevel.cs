using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerInLevel : MonoBehaviour
{
    GameObject player;
    public Vector3 positionToMove;
    ObjectiveManager objManager;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectiveManager>();
    }
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        objManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObjectiveManager>();
    }
    // Update is called once per frame
    public void MovePlayer()
    {
        player.transform.position = positionToMove;
        objManager.MoveObjectiveComplete();
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        MovePlayer();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        MovePlayer();
    }
}
