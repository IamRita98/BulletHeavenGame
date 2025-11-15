using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeaponStats : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] float baseDamage = 0;
    [SerializeField] float fireRate = 0;
    [SerializeField] float lifeTime = 0;
    [SerializeField] float area = 0;
    
    public Stats BaseDamage;
    public Stats LifeTime;
    public Stats FireRate;
    public Stats Area;

    private void Start()
    {
        MakeStats();
    }

    private void MakeStats()
    {
        BaseDamage = new Stats(baseDamage);
        LifeTime = new Stats(lifeTime);
        FireRate = new Stats(fireRate);
        Area = new Stats(area);
    }
}
