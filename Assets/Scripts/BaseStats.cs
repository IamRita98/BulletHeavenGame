using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BaseStats : MonoBehaviour
{
    [SerializeField] float health = 0;
    [SerializeField] float attackPower = 0;
    [SerializeField] float projectiles = 0;
    [SerializeField] float fireRate = 0;
    [SerializeField] float speed = 0;
    public Stats AttackPower;
    public Stats MovementSpeed;
    public Stats Health;
    public Stats FireRate;
    public Stats Projectiles;

    private void Start()
    {
        MakeStats();
        
    }
    private void MakeStats()
    {
        MovementSpeed = new Stats(speed);
        Health = new Stats(health);
        AttackPower = new Stats(attackPower);
    }
}
