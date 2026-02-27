using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    GameStateManager gameStateManager;

    public static event System.Action<GameObject> OnEnemyDeath;
    public static event System.Action OnPlayerDeath;
    ObjectPooling oPool;
    float playerInvincibilityDuration;
    public bool shortInvinc = false;
    public bool isShortInvinc = false;
    public bool isLongInvinc = false;
    public bool shouldBeInvinc = false;
    public bool shouldExplode = false;
    float invincibilityTimer;

    private void Awake()
    {
        gameStateManager = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<GameStateManager>();
        oPool = gameObject.GetComponent<ObjectPooling>();
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
                    ShouldExplode(gObject,dam);
                }
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
    void ShouldExplode(GameObject gObject,float dam)
    {
        int roll = HandleRoll();
        if (roll >= 75)
        {
            GameObject explodeCircle = oPool.objectPool[0];
            oPool.activePool.Add(explodeCircle);
            oPool.objectPool.Remove(explodeCircle);
            explodeCircle.transform.position = gObject.transform.position;
            GenericOnEnterDam genDam = gameObject.GetComponent<GenericOnEnterDam>();
            genDam.damage = dam * .3f;
            explodeCircle.SetActive(true);
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
        if (shouldBeInvinc)return;
        BaseStats pbs = GO.GetComponent<BaseStats>();
        playerInvincibilityDuration = pbs.invincibilityDuration; 
        pbs.Health.AddFlatValue(-damage);
        InvincibilityDuration(playerInvincibilityDuration);
        //StartCoroutine(InvincibilityWindow(playerInvincibilityDuration));
        if(pbs.Health.StatsValue() <= 0) OnPlayerDeath?.Invoke();
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
