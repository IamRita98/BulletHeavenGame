using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseStats : BaseStats
{
    [SerializeField] float contactDamage;
    [SerializeField] float attackDamage;

    public Stats ContactDamage;
    public Stats AttackDamage;

    void Start()
    {
        MakeStats();
    }

    void Update()
    {
        
    }

    private void MakeStats()
    {
        ContactDamage = new Stats(contactDamage);
        AttackDamage = new Stats(attackDamage);
    }
}
