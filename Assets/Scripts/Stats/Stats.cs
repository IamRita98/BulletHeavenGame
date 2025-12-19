using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    float baseValue;
    float bonusValue;
    float multiplier = 1;
    int tier;

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
        multiplier *= valueMult;
    }
    public void AddFlatMultiValue(float valueMult)
    {
        multiplier += valueMult;
    }
    public Stats(float passedbaseValue)
    {
        baseValue = passedbaseValue;
    }
    public void StatOverwrite(float passedbaseValue)
    {
        baseValue = passedbaseValue;
    }

}
