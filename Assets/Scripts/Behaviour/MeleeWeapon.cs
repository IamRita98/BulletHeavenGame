using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MeleeWeapon : MonoBehaviour
{
    ObjectPooling oPool;
    BaseWeaponStats bws;
    float timer;
    bool inCombat = false;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        if (SceneManager.GetActiveScene().name != "MainMenu") GetReferences(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
    }

    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        inCombat = true;
        oPool = GameObject.FindGameObjectWithTag("ProjectilePool").GetComponent<ObjectPooling>();
        bws = gameObject.GetComponent<BaseWeaponStats>();
    }

    void Update()
    {
        if (!inCombat) return;
        timer += Time.deltaTime;
        if (timer >= bws.AttackRate.StatsValue())
        {
            timer = 0f;
            AimWeapon();
        }
    }

    private void AimWeapon()
    {
        GameObject tne = gameObject.GetComponentInParent<TrackNeareastEnemy>().NearestEnemy();
        Vector2 targetPos = tne.transform.position - transform.position;
        SwingWeapon(targetPos);
    }

    private void SwingWeapon(Vector2 targetPos)
    {
        float projectiles = bws.Projectiles.StatsValue();

        for (int i = 0; i < projectiles; i++)//for spread
        {
            GameObject meleeCleave = SpawnMeleeAttack();
            GiveWeaponDirection(meleeCleave, targetPos);
        }
    }
    GameObject SpawnMeleeAttack()
    {
        GameObject meleeGO = oPool.objectPool[0];
        meleeGO.GetComponentInChildren<ProjectileBehaviour>().SetStats(bws.BaseDamage.StatsValue(), bws.WeapArea.StatsValue(), bws.LifeTime.StatsValue(), bws.ProjectileSpeed.StatsValue(), bws.Pierce.StatsValue());
        print("Attack dam: " + bws.BaseDamage.StatsValue());
        meleeGO.SetActive(true);
        oPool.activePool.Add(meleeGO);
        oPool.objectPool.Remove(meleeGO);
        return meleeGO;
    }
    void GiveWeaponDirection(GameObject cleave, Vector2 targetPos)
    {

        cleave.transform.up = targetPos;
        cleave.transform.position = transform.position;
    }
}

