using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class TrackNeareastEnemy : MonoBehaviour
{
    ObjectPooling oPool;
    private GameObject nearestEnemyV2;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += GetReferences;
        if (SceneManager.GetActiveScene().name != "MainMenu") GetReferences(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
}

    void GetReferences(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "MainMenu") return;
        oPool = GameObject.FindGameObjectWithTag("EnemyPool").GetComponent<ObjectPooling>();
    }

    public GameObject NearestEnemy()
    {
        float dist=Mathf.Infinity;
        if (oPool.activePool.Count < 1) return Camera.main.gameObject;

        for (int i = 0; i < oPool.activePool.Count; i++)//there may be a more efficient way (2b optimized)
        {
            Vector2 directionToTarget = oPool.activePool[i].transform.position - gameObject.transform.position;
            float dSqrTT = directionToTarget.sqrMagnitude;
            if (dSqrTT< dist)
            {
                dist = dSqrTT;
                nearestEnemyV2 = oPool.activePool[i];
            }
        }
        return nearestEnemyV2;
    }
}
