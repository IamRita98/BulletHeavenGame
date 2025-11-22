using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BaseStats : MonoBehaviour
{
    //Some stats such as a global damage increase need to be applied specifically to the Weapon and Abilities seperately
    [SerializeField] float health = 0;
    [SerializeField] float maxHealth = 0;
    [SerializeField] float speed = 0;
    [SerializeField] float projectiles = 0;

    public Stats Health;
    public Stats MaxHealth;
    public Stats MovementSpeed;
    public Stats Projectiles;

    private void Start()
    {
        MakeStats();
    }

    private void MakeStats()
    {
        Health = new Stats(health);
        MaxHealth = new Stats(maxHealth);
        MovementSpeed = new Stats(speed);
        Projectiles = new Stats(projectiles);
    }
}
