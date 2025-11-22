using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseAbilityStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float abilitySpeed = 0;
    [SerializeField] float baseDamage = 0;
    [SerializeField] float lifeTime= 0;
    [SerializeField] float abilityArea = 0;
    [SerializeField] float cooldown = 0;
    public Stats BaseDamage;
    public Stats LifeTime;
    public Stats AbilitySpeed;
    public Stats AbilityArea;
    public Stats Cooldown;
    private void Start()
    {
        MakeStats();

    }
    private void MakeStats()
    {
        LifeTime = new Stats(lifeTime);
        AbilitySpeed = new Stats(abilitySpeed);
        BaseDamage = new Stats(baseDamage);
        Cooldown = new Stats(cooldown);
        AbilityArea = new Stats(abilityArea);
    }
}
