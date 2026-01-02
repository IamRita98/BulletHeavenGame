using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CombatHandler : MonoBehaviour
{
    GameStateManager gameStateManager;

    public static event System.Action<GameObject> OnEnemyDeath;
    public static event System.Action OnPlayerDeath;

    float playerInvincibilityDuration;
    bool shouldBeInvinc = false;
    float invincabilityTimer;


    void GetReferences(Scene scene)
    {
        gameStateManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>();
    }

    private void OnEnable()
    {
        ContactDamage.OnCollision += CollisionDamage;
        SceneManagerScript.LevelLoaded += GetReferences;
    }

    private void OnDisable()
    {
        ContactDamage.OnCollision -= CollisionDamage;
        SceneManagerScript.LevelLoaded -= GetReferences;
    }

    private void Update()
    {
        if (!shouldBeInvinc) return;
        invincabilityTimer += Time.deltaTime;
        if (invincabilityTimer >= playerInvincibilityDuration) shouldBeInvinc = false;
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
        BaseStats pbs = GO.GetComponent<BaseStats>();
        playerInvincibilityDuration = pbs.invincibilityDuration;
        if (shouldBeInvinc) return;
        pbs.Health.AddFlatValue(-damage);
        if(pbs.Health.StatsValue() <= 0) OnPlayerDeath?.Invoke();
        shouldBeInvinc = true;
    }
}
