using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public static event System.Action<GameObject> OnEnemyDeath;
    public static event System.Action OnPlayerDeath;

    float playerInvincibilityDuration;
    bool shouldBeInvinc = false;
    float invincabilityTimer;

    private void OnEnable()
    {
        ProjectileBehaviour.OnAttackHit += HandleDamage;
        ContactDamage.OnCollision += CollisionDamage;
    }
    private void OnDisable()
    {
        ProjectileBehaviour.OnAttackHit -= HandleDamage;
        ContactDamage.OnCollision -= CollisionDamage;
    }

    private void Update()
    {
        if (!shouldBeInvinc) return;
        invincabilityTimer += Time.deltaTime;
        if (invincabilityTimer >= playerInvincibilityDuration) shouldBeInvinc = false;
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
                OnEnemyDeath?.Invoke(gObject);
                ebs.ReturnToPool();
            }
        }
        else if (gObject.CompareTag("Player"))
        {
            //player stats calcs
        }
    }

    void CollisionDamage(float damage, GameObject GO)
    {
        BaseStats pbs = GO.GetComponent<BaseStats>();
        playerInvincibilityDuration = pbs.invincibilityDuration;
        if (shouldBeInvinc) return;
        pbs.Health.AddFlatValue(-damage);
        print(pbs.Health.StatsValue() + "/" + pbs.MaxHealth.StatsValue() + "Player HP");
        if(pbs.Health.StatsValue() <= 0) OnPlayerDeath?.Invoke();
        shouldBeInvinc = true;
    }
}
