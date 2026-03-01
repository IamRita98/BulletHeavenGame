using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContactDamage : MonoBehaviour
{
    public static event System.Action<float, GameObject> OnCollision;
    CombatHandler combatHandler;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
    }

    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        EnemyBaseStats ebs = collision.gameObject.GetComponent<EnemyBaseStats>();
        combatHandler.HandleDamage(ebs.ContactDamage.StatsValue(), gameObject,CombatHandler.DamageType.Physical);
        //OnCollision?.Invoke(damage, gameObject);
    }
}
