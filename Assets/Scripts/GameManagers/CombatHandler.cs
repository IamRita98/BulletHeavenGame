using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    GameStateManager gameStateManager;

    public static event System.Action<GameObject> OnEnemyDeath;
    public static event System.Action OnPlayerDeath;

    float playerInvincibilityDuration;
    public bool shouldBeInvinc = false;
    float invincabilityTimer;

    private void Awake()
    {
        gameStateManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<GameStateManager>();
    }

    private void OnEnable()
    {
        ContactDamage.OnCollision += CollisionDamage;
    }

    private void OnDisable()
    {
        ContactDamage.OnCollision -= CollisionDamage;
    }

    private void Update()
    {
        //if (!shouldBeInvinc) return;
        //invincabilityTimer += Time.deltaTime;
        //if (invincabilityTimer >= playerInvincibilityDuration) shouldBeInvinc = false;
    }

    public void HandleDamage(float dam, GameObject gObject)
    {
        if (gObject.CompareTag("Enemy"))
        {
            EnemyBaseStats ebs = gObject.GetComponent<EnemyBaseStats>();
            ebs.Health.AddFlatValue(-dam);
            print(gObject + ": " + ebs.Health.StatsValue() + "/" + ebs.MaxHealth.StatsValue() + "hp");
            if (ebs.Health.StatsValue() <= 0)
            {
                OnEnemyDeath?.Invoke(gObject);
                ebs.ReturnToPool();
            }
        }
        else if (gObject.CompareTag("Player"))
        {
            //player stats calcs
            //if (pbs.Health.StatsValue() <= 0) OnPlayerDeath?.Invoke();
        }
    }

    void CollisionDamage(float damage, GameObject GO)
    {
        if (shouldBeInvinc) return;
        BaseStats pbs = GO.GetComponent<BaseStats>();
        playerInvincibilityDuration = pbs.invincibilityDuration; 
        pbs.Health.AddFlatValue(-damage);
        shouldBeInvinc = true;
        StartCoroutine(InvincibilityWindow(playerInvincibilityDuration));
        if(pbs.Health.StatsValue() <= 0) OnPlayerDeath?.Invoke();
    }
    public IEnumerator InvincibilityWindow(float duration)
    {
        yield return new WaitForSeconds(duration);
        shouldBeInvinc = false;
    }
}
