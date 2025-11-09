using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private float baseValue;
    private float bonusValue;
    private float multiplier;

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
}
