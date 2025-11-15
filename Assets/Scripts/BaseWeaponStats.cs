using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeaponStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float projectilesSpeed= 0;
    [SerializeField] float baseDamage = 0;
    [SerializeField] float lifeTime = 0;
    [SerializeField] float projectileSize = 0;
    [SerializeField] float pierce= 0;
    public Stats BaseDamage;
    public Stats LifeTime;
    public Stats ProjectileSpeed;
    public Stats ProjectileSize;
    public Stats Pierce;
    private void Start()
    {
        MakeStats();

    }
    private void MakeStats()
    {
        LifeTime = new Stats(lifeTime);
        ProjectileSpeed= new Stats(projectilesSpeed);
        BaseDamage= new Stats(baseDamage);
        Pierce = new Stats(pierce);
        ProjectileSize = new Stats(projectileSize);
    }
}
