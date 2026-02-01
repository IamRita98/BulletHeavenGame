using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChillElement : MonoBehaviour
{
    void Awake()
    {
        
    }
    /// <summary>
    /// (Pass strength as a decimal ie: .6)Apply Debuffs takes in a target along with strength of slow and duration
    /// </summary>
    /// <param name="enemyGO">Object to be debuffed</param>
    /// <param name="strengthOfSlow">Pass as 0.5 to half enemy stat</param>
    /// <param name="slowDuration">Duration of debuff</param>
    public void ApplyDebuffs(GameObject enemyGO,float strengthOfSlow,float slowDuration)
    {
        StartCoroutine(ApplySlow(enemyGO,strengthOfSlow,slowDuration));
    }
    IEnumerator ApplySlow(GameObject enemyGO,float strengthOfSlow, float slowDuration)
    {
        enemyGO.GetComponent<EnemyBaseStats>().MovementSpeed.AddMultiValue(strengthOfSlow);
        yield return new WaitForSeconds(1);
        enemyGO.GetComponent<EnemyBaseStats>().MovementSpeed.AddMultiValue((1 / strengthOfSlow));
    }
}
