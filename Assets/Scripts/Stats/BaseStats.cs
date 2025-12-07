using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;



public class BaseStats : MonoBehaviour
{
    public enum Character
    {
        DefaultDaniel,
        SarahSword,
        Enemy,
    }

    public Character characterSelected;

    //Some stats such as a global damage increase need to be applied specifically to the Weapon and Abilities seperately
    [SerializeField] float health = 0;
    [SerializeField] float maxHealth = 0;
    [SerializeField] float speed = 1;
    [SerializeField] float projectiles = 1;
    [SerializeField] float xp = 0;
    [SerializeField] float pickUpRadius= 0;
    public float invincibilityDuration;

    public Stats Health;
    public Stats MaxHealth;
    public Stats MovementSpeed;
    public Stats Projectiles;
    public Stats XP;
    public Stats PickupRadius;

    //upgrade tiers trackers
    public int globalDamT = 0;
    public int healthUpgT = 0;
    public int movementSpUpgT = 0;
    public int projUpgT = 0;
    public int XpPickupUpgT = 0;


    private void Awake()
    {
        MakeStats();
    }

    private void Start()
    {
/*        switch (characterSelected)
        {
            case (Character.DefaultDaniel):
                break;
        }*/
    }

    private void MakeStats()
    {
        Health = new Stats(health);
        MaxHealth = new Stats(maxHealth);
        MovementSpeed = new Stats(speed);
        Projectiles = new Stats(projectiles);
        XP = new Stats(xp);
        PickupRadius = new Stats(pickUpRadius);
    }
}
