using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField]float XPThreshold=10;//base value
    BaseStats playerStats;
    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
    }
    private void OnEnable()
    {
        PickupBehavior.OnXPPickup += HandleXPPickup;
    }
    private void OnDisable()
    {
        PickupBehavior.OnXPPickup += HandleXPPickup;
    }
    void HandleXPPickup(float XPValue)
    {
        playerStats.XP.AddFlatValue(XPValue);
        CheckIfLeveled();
    }
    void CheckIfLeveled()
    {
        if (playerStats.XP.StatsValue() > +XPThreshold)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        //other things
        playerStats.XP.AddFlatValue(-XPThreshold);//subtract level up xp from current avoid overwriting
        ScalingThreshold();
    }
    void ScalingThreshold()
    {
        XPThreshold *= 2;
    }
}
