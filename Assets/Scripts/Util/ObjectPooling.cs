using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject poolObject;
    [SerializeField] int objectAmount=1;
    public List<GameObject> objectPool = new List<GameObject>();
    public List<GameObject> activePool = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < objectAmount; i++)
        {
            GameObject spawnedGO = Instantiate(poolObject);
            objectPool.Add(spawnedGO);
            spawnedGO.transform.SetParent(gameObject.transform);
            spawnedGO.SetActive(false);
        }    
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += UnattachFromParent;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= UnattachFromParent;
    }

    void UnattachFromParent(Scene oldScene, Scene newScene)
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (transform.parent != null) transform.parent = null;
        }
    }
}
