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
    Vector2 mousePos;
    float timer = 0;
    bool firstTimeChecked = false;
    private void Awake()
    {
        abilityStats = gameObject.GetComponent<AbilityStats>();
    }
    private void Update()
    {
        if (timer >= duration)
        {
            firstTimeChecked = true;
            gameObject.SetActive(false);
            timer = 0;
        }
        timer += Time.deltaTime;
    }
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        print("target pos is: " + targetPos);
        duration = abilityStats.LifeTime.StatsValue();
        if (!firstTimeChecked) return;
        combatHandler.shouldBeInvinc = true;
        StartCoroutine(combatHandler.InvincibilityWindow(duration));
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= GetReferences;
    }
    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        
    }
}
