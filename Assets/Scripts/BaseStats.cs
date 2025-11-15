using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BaseStats : MonoBehaviour
{
    //Some stats such as a global damage increase need to be applied specifically to the Weapon and Abilities seperately
    [SerializeField] float health = 0;
    [SerializeField] float projectiles = 0;
    [SerializeField] float speed = 0;

    public Stats MovementSpeed;
    public Stats Health;
    public Stats Projectiles;

    private void Start()
    {
        MakeStats();
    }

    private void MakeStats()
    {
        MovementSpeed = new Stats(speed);
        Health = new Stats(health);
        Projectiles = new Stats(projectiles);
    }
}
