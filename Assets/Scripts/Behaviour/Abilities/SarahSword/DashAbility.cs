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
    float time=0;
    private void Awake()
    {
        abilityStats = gameObject.GetComponent<AbilityStats>();
    }
    private void Update()
    {
        if (time >= duration)
        {
            gameObject.SetActive(false);
            time = 0;
        }
        time += Time.deltaTime;
    }
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos = new Vector2(mousePos.x, mousePos.y);
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
        combatHandler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CombatHandler>();
        
    }
}
