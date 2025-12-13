using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStats : BaseStats
{
    [SerializeField] float baseDamage = 0;
    [SerializeField] float cooldown = 0;
    [SerializeField] float lifeTime = 0;
    [SerializeField] float area = 0;
    [SerializeField] float pierce = 0;
    [SerializeField] float projectileSpeed;

    public Stats BaseDamage;
    public Stats Cooldown;
    public Stats LifeTime;
    public Stats Area;
    public Stats Pierce;
    public Stats ProjectileSpeed;

    private void Start()
    {
        MakeStats();
    }

    private void MakeStats()
    {
        BaseDamage = new Stats(baseDamage);
        LifeTime = new Stats(lifeTime);
        Cooldown = new Stats(cooldown);
        Area = new Stats(area);
        ProjectileSpeed = new Stats(projectileSpeed);
        Pierce = new Stats(pierce);
    }
}
