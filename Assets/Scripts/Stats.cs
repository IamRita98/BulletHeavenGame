using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    [SerializeField] float baseValue;
    [SerializeField]  float bonusValue;
    [SerializeField]  float multiplier;

    public float GetValue()
    {
        return (baseValue + bonusValue) * multiplier;
    }
    public float StatsValue()
    {
        return (baseValue + bonusValue) * multiplier;
    }

    public void AddFlatValue(float addedValue)
    {
        bonusValue += addedValue;
    }

    public void AddMultiValue(float valueMult)
    {
        multiplier += valueMult;
    }
    public Stats(float baseValue)
    {
        this.baseValue = baseValue;
    }
}
