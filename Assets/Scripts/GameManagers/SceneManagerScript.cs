using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static event System.Action<Scene> LevelLoaded;
    public void ReloadLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }
    public void StartLevel()
    {
        SceneManager.LoadScene("TestLevel");
        LevelLoaded?.Invoke(SceneManager.GetActiveScene());
    }
}
