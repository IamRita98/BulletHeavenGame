using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChillElement : MonoBehaviour
{
    EnemyBaseStats ebs;
    float slowDuration;
    float strength;
    bool isSlowed = false;
    float timer = 0;
    bool currentlyDebuffed;

    void Awake()
    {
        ebs = gameObject.GetComponent<EnemyBaseStats>();
    }
    /// <summary>
    /// (Pass strength as a decimal ie: .6)Apply Debuffs takes in a target along with strength of slow and duration
    /// </summary>
    /// <param name="strengthOfSlow">Pass as 0.5 to half enemy stat</param>
    /// <param name="slowDuration">Duration of debuff</param>
    public void SetDebuffs(float strengthOfSlow, float slowDur)
    {
        if (timer < slowDur) timer = 0;
        else return;
        strength = strengthOfSlow;
        slowDuration = slowDur;
        isSlowed = true;
        timer = 0;
    }
    void Update()
    {
        if (currentlyDebuffed)
        {
            if (timer >= slowDuration)
            {
                RemoveSlow();
                isSlowed = false;
            }
        }
        if (isSlowed)
        {
            ApplyDebuff();
            isSlowed = false;
        }
        timer += Time.deltaTime;
    }
    void ApplyDebuff()
    {
        ebs.MovementSpeed.AddMultiValue(strength);
        currentlyDebuffed = true;
    }
    void RemoveSlow()
    {
        timer = 0;
        isSlowed = false;
        ebs.MovementSpeed.AddMultiValue(1 / strength);
        currentlyDebuffed = false;
        TurnOff();
    }
    private void TurnOff()
    {
        ChillElement cE = gameObject.GetComponent<ChillElement>();
        cE.enabled = false;
    }
}
