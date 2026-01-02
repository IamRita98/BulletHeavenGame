using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public bool gameIsPaused = false;

    private void OnEnable()
    {
        CombatHandler.OnPlayerDeath += PauseGame;
    }
    private void OnDisable()
    {
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
    public void ExitGame()
    {
        Application.Quit();
    }
}
