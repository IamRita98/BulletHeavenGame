using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public enum DamageType
    {
        Physical,
        Poison,
        Fire,
        Light,
        Untyped,
    }
    public DamageType dType;
    GameStateManager gameStateManager;
    FloatingDamageBehaviour fDB;
    public static event System.Action<GameObject> OnEnemyDeath;
    public static event System.Action OnPlayerDeath;
    ObjectPooling explOPool;
    ObjectPooling oPoolText;
    ObjectPooling enemyPool;
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
    public bool hasRevives = false;
    float reviveTimer = 0;
    public int revives = 0;
    int fireRateStackAmount = 1;
    float invincibilityTimer;
    BaseStats pbs;
    PlaySFXAfterFirstEnable sfxPlayer;



    private void Awake()
    {
        gameStateManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<GameStateManager>();
        explOPool = gameObject.GetComponent<ObjectPooling>();
        oPoolText = GameObject.FindGameObjectWithTag("FloatingDamageNumbersPool").GetComponent<ObjectPooling>();
        fireRateStackingUpgrade = gameObject.GetComponent<FireRateStackingUpgrade>();
        playerSRend = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        pbs = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        enemyPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPooling>();
    }

    private void Update()
    {
        if (hasRevives) reviveTimer++;
        if (reviveTimer >= 600f)
        {
            reviveTimer = 0;
            revives++;
        }
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
    public void HandleDamage(float dam, GameObject gObject,DamageType type)
    {
        dType = type;
        if (gObject.CompareTag("Enemy"))
        {
            FloatingTextNum(dam, gObject,type);
            ReturnToPoolOnDeath returnToPool = gObject.GetComponent<ReturnToPoolOnDeath>();
            BaseStats ebs = gObject.GetComponent<BaseStats>();
            ebs.Health.AddFlatValue(-dam);
            print(gObject + ": " + ebs.Health.StatsValue() + "/" + ebs.MaxHealth.StatsValue() + "hp");
            if (ebs.Health.StatsValue() <= 0)
            {
                if (shouldExplode)
                {
                    StartCoroutine(ShouldExplode(gObject, dam));
                }
                OnEnemyDeath?.Invoke(gObject);
                returnToPool.ReturnToPool();
                sfxPlayer = ebs.gameObject.GetComponent<PlaySFXAfterFirstEnable>();
                sfxPlayer.playSFX();
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
                dType = DamageType.Untyped;
                FloatingTextNum(dam, gObject,type);
            }
            damageTakenVFX = gObject.GetComponentInChildren<DamageTakenVFX>();
            damageTakenVFX.DamageTaken(gObject);
        }

        else if (gObject.CompareTag("Player"))
        {
            if (shouldBeInvinc) return;
            FloatingTextNum(dam, gObject,type);
            BaseStats pbs = gObject.GetComponent<BaseStats>();
            playerInvincibilityDuration = pbs.invincibilityDuration;
            pbs.Health.AddFlatValue(-dam);
            InvincibilityDuration(playerInvincibilityDuration);
            if (pbs.Health.StatsValue() <= 0)
            {
                Debug.Log("Max hp: "+pbs.MaxHealth.StatsValue());
                if (revives >= 1)
                {
                    Revive();
                    ClearScreen();
                }
                else
                {
                    OnPlayerDeath?.Invoke();
                    return;
                }
                    
            }
            damageTakenVFX = playerSRend.GetComponent<DamageTakenVFX>();
            damageTakenVFX.DamageTaken(gObject);
            sfxPlayer = pbs.gameObject.GetComponent<PlaySFXAfterFirstEnable>();
            sfxPlayer.playSFX();
        }
    }
    public void ClearScreen()
    {
        /*        foreach(GameObject enemy in enemyPool.activePool)
                {
                    ReturnToPoolOnDeath returnToPool = enemy.GetComponent<ReturnToPoolOnDeath>();
                    print(enemy);
                    returnToPool.ReturnToPool();
                }*/
        int temp = enemyPool.activePool.Count;
        for (int i = 0; i < temp; i++)
        {
            GameObject enemy = enemyPool.activePool[0];
            ReturnToPoolOnDeath returnToPool = enemy.GetComponent<ReturnToPoolOnDeath>();
            returnToPool.ReturnToPool();
        }
    }
    void Revive()
    {
        /*
        * We set HP back to 0 if negative and find the difference between the goalHealth & the health value with mult
        * & bonuses added (Health.StatsValue()) to find the true value required to set HP back to half of max
        */
        float goalHealth = pbs.MaxHealth.StatsValue() / 2;
        pbs.Health.AddFlatValue(-1 * pbs.Health.StatsValue());
        pbs.Health.AddFlatValue(goalHealth);
        float div = pbs.Health.StatsValue();
        pbs.Health.AddFlatValue(-goalHealth);
        float diff = div - goalHealth;
        Debug.Log("curr hp: " + pbs.Health.StatsValue() + " Div:" + div);
        pbs.Health.AddFlatValue(Mathf.Abs(diff));
    }
    void FloatingTextNum(float dam, GameObject gObject,DamageType type)
    {
        GameObject textGO = oPoolText.objectPool[0];
        oPoolText.activePool.Add(textGO);
        oPoolText.objectPool.Remove(textGO);
        TMP_Text text = textGO.GetComponent<TMP_Text>();
        dam = (int)dam;
        text.text = dam.ToString();
        fDB = textGO.GetComponent<FloatingDamageBehaviour>();
        
        //text.transform.SetParent(gObject.transform);
        text.transform.position = gObject.transform.position;
        text.transform.position=new Vector2(text.transform.position.x,text.transform.position.y+1);
        textGO.SetActive(true);
        fDB.Setup(text, type);
    }

    IEnumerator ShouldExplode(GameObject gObject, float dam)
    {
        int roll = HandleRoll();
        GameObject explodeCircle = explOPool.objectPool[0];
        if (roll >= 75)
        {
            explOPool.activePool.Add(explodeCircle);
            explOPool.objectPool.Remove(explodeCircle);
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
}
