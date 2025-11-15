using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BaseStats : MonoBehaviour
{
    [SerializeField] float health=10f;
    [SerializeField] float attackPower= 5f;
    [SerializeField] private Stats Health;
    [SerializeField] private Stats AttackPower;
    private void Start()
    {
        Health = new Stats(health);
        AttackPower = new Stats(attackPower);
    }
    
}
