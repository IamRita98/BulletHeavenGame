using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BaseStats : MonoBehaviour
{
    [SerializeField] private Stats AttackPower;

    float speed = 0;
    public Stats MovementSpeed;

    float health = 0;
    [SerializeField] Stats Health;

    private void Start()
    {
        MovementSpeed = new Stats(speed);
        Health = new Stats(health);

        print(Health.StatsValue());
        print(AttackPower.StatsValue());
        //AttackPower = new Stats(attackPower);
    }
}
