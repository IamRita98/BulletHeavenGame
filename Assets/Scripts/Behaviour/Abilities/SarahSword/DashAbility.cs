using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DashAbility : MonoBehaviour
{
    Transform playerTransf;
    CombatHandler combatHandler;
    AbilityStats abilityStats;
    float duration;
    Vector2 targetPos;
    private void Awake()
    {
        abilityStats = gameObject.GetComponent<AbilityStats>();
    }
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        targetPos = gameObject.transform.parent.GetComponent<Transform>().position*2;
        print("target pos is: " + targetPos);
        duration = abilityStats.LifeTime.StatsValue();
        combatHandler.StartCoroutine(combatHandler.InvincibilityWindow(duration));
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
    }
    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        combatHandler = gameObject.GetComponentInParent<CombatHandler>();
        
    }
}
