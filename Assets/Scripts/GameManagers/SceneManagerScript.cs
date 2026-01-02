using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void ReloadLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }
}
