using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateStackingUpgrade : MonoBehaviour
{
    const int MAXSTACKS = 35;
    public Dictionary<GameObject, int> enemyDamageStacks = new Dictionary<GameObject, int>();

    public void IncrementValue(GameObject goHit, int valueToAdd)
    {
        if (enemyDamageStacks[goHit] == MAXSTACKS) return;
        else if (enemyDamageStacks[goHit] > MAXSTACKS)
        {
            enemyDamageStacks[goHit] = MAXSTACKS;
            return;
        }
        
        enemyDamageStacks[goHit] += valueToAdd;
    }
}
