using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    private void OnEnable()
    {
        ProjectileBehaviour.OnAttackHit += HandleDamage;
    }
    private void OnDisable()
    {
        ProjectileBehaviour.OnAttackHit -= HandleDamage;
    }
    void HandleDamage(float dam, GameObject gObject)
    {
        if (gObject.CompareTag("Enemy"))
        {
            EnemyBaseStats ebs = gObject.GetComponent<EnemyBaseStats>();
            ebs.Health.AddFlatValue(-dam);
            print(ebs.Health.StatsValue() + "/" + ebs.MaxHealth.StatsValue());
            if (ebs.Health.StatsValue() <= 0)
            {
                ebs.ReturnToPool();
            }
        }
        else
        {
            //player stats calcs
        }

    }
}
