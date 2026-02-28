using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    GameStateManager gameStateManager;
    FloatingDamageBehaviour fDB;
    public static event System.Action<GameObject> OnEnemyDeath;
    public static event System.Action OnPlayerDeath;
    ObjectPooling oPool;
    ObjectPooling oPoolText;
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
        oPoolText = GameObject.FindGameObjectWithTag("FloatingDamageNumbersPool").GetComponent<ObjectPooling>();
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
            FloatingTextNum(dam, gObject);
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
            FloatingTextNum(dam, gObject);
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
    void FloatingTextNum(float dam, GameObject gObject)
    {
        GameObject textGO = oPoolText.objectPool[0];
        oPoolText.activePool.Add(textGO);
        oPoolText.objectPool.Remove(textGO);
        textGO.SetActive(true);
        TMP_Text text = textGO.GetComponent<TMP_Text>();
        dam = (int)dam;
        text.text = dam.ToString();
        text.transform.SetParent(gObject.transform);
        text.transform.position = gObject.transform.position;
        text.transform.position=new Vector2(text.transform.position.x,text.transform.position.y+1);
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
