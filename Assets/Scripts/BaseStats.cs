using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class BaseStats : MonoBehaviour
{
    [SerializeField] float health=10f;
    [SerializeField] float attackPower= 5f;
    private Stats Health;
    private Stats AttackPower;
    private void Start()
    {
        Health = new Stats(health);
        AttackPower = new Stats(attackPower);
    }
    
}
