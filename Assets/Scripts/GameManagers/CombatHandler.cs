using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    GameStateManager gameStateManager;

    public static event System.Action<GameObject> OnEnemyDeath;
    public static event System.Action OnPlayerDeath;
    ObjectPooling oPool;
    SpriteRenderer playerSRend;
    FireRateStackingUpgrade fireRateStackingUpgrade;
    DamageTakenVFX damageTakenVFX;
    float playerInvincibilityDuration;
    public bool shortInvinc = false;
    public bool isShortInvinc = false;
    public bool isLongInvinc = false;
    public bool shouldBeInvinc = false;
    public bool shouldExplode = false;
    public bool fireRateTier3 = false;
    int fireRateStackAmount = 1;
    float invincibilityTimer;

    private void Awake()
    {
        gameStateManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<GameStateManager>();
        oPool = gameObject.GetComponent<ObjectPooling>();
        fireRateStackingUpgrade = gameObject.GetComponent<FireRateStackingUpgrade>();
        playerSRend = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
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
        //handling invincibility
        if (!shouldBeInvinc) return;
        invincibilityTimer += Time.deltaTime;
        if (invincibilityTimer >= playerInvincibilityDuration)
        {
            shouldBeInvinc = false;
            invincibilityTimer = 0;
        }
    }

    /// <summary>
    /// Pass Damage taken and GO being hit
    /// </summary>
    /// <param name="dam"></param>
    /// <param name="gObject"></param>
    public void HandleDamage(float dam, GameObject gObject)
    {
        if (gObject.CompareTag("Enemy"))
        {
            EnemyBaseStats ebs = gObject.GetComponent<EnemyBaseStats>();
            ebs.Health.AddFlatValue(-dam);
            print(gObject + ": " + ebs.Health.StatsValue() + "/" + ebs.MaxHealth.StatsValue() + "hp");
            if (ebs.Health.StatsValue() <= 0)
            {
                if (shouldExplode)
                {
                    StartCoroutine(ShouldExplode(gObject, dam));
                }
                OnEnemyDeath?.Invoke(gObject);
                ebs.ReturnToPool();
                return;
            }
            else if (fireRateTier3)
            {
                if (fireRateStackingUpgrade.enemyDamageStacks.ContainsKey(gObject))
                {
                    ebs.Health.AddFlatValue(-fireRateStackingUpgrade.enemyDamageStacks[gObject]);
                    fireRateStackingUpgrade.IncrementValue(gObject, fireRateStackAmount);
                }
                else
                {
                    fireRateStackingUpgrade.enemyDamageStacks.Add(gObject, fireRateStackAmount);
                }
                print("Stacking firerate dmg: " + fireRateStackingUpgrade.enemyDamageStacks[gObject]);
            }
            damageTakenVFX = gObject.GetComponentInChildren<DamageTakenVFX>();
            damageTakenVFX.DamageTaken(gObject);
        }

        else if (gObject.CompareTag("Player"))
        {
            if (shouldBeInvinc) return;
            BaseStats pbs = gObject.GetComponent<BaseStats>();
            playerInvincibilityDuration = pbs.invincibilityDuration;
            pbs.Health.AddFlatValue(-dam);
            InvincibilityDuration(playerInvincibilityDuration);
            if (pbs.Health.StatsValue() <= 0)
            {
                OnPlayerDeath?.Invoke();
                return;
            }
            damageTakenVFX = playerSRend.GetComponent<DamageTakenVFX>();
            damageTakenVFX.DamageTaken(gObject);
        }
    }

    IEnumerator ShouldExplode(GameObject gObject, float dam)
    {
        int roll = HandleRoll();
        GameObject explodeCircle = oPool.objectPool[0];
        if (roll >= 75)
        {
            oPool.activePool.Add(explodeCircle);
            oPool.objectPool.Remove(explodeCircle);
            explodeCircle.transform.position = gObject.transform.position;
            GenericOnEnterDam genDam = explodeCircle.GetComponent<GenericOnEnterDam>();
            genDam.damage = dam * .3f;
            explodeCircle.SetActive(true);
            yield return new WaitForSeconds(.4f);
            explodeCircle.SetActive(false);
        }

    }
    int HandleRoll()
    {
        int rolled = Random.Range(1, 101);
        return rolled;
    }
    public void InvincibilityDuration(float newInvincDuration)//checking to see if we need to update to a longer window
    {
        if (playerInvincibilityDuration <= newInvincDuration)
        {
            playerInvincibilityDuration = newInvincDuration;
            shouldBeInvinc = true;
            invincibilityTimer = 0;
        }
        
    }

    void CollisionDamage(float damage, GameObject GO)
    {
        
    }
    //public IEnumerator InvincibilityWindow(float duration)
    //{
    //    print("Im frustrated");
    //    yield return new WaitForSeconds(duration);
    //    if (isShortInvinc)
    //    {
    //        shortInvinc = false;
    //        isShortInvinc = false;
    //    }
    //    if (isLongInvinc)
    //    {
    //        shouldBeInvinc = false;
    //        isLongInvinc = false;
    //    }
        
    //}
}
