using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public bool gameIsPaused = false;
    GameObject player;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneChanged;
        CombatHandler.OnPlayerDeath += PauseGame;
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneChanged;
        CombatHandler.OnPlayerDeath -= PauseGame;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void SceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = Vector3.zero;
    }
}
