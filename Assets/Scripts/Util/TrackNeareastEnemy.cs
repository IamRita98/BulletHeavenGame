using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class TrackNeareastEnemy : MonoBehaviour
{
    ObjectPooling oPool;
    private GameObject nearestEnemy;

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

    /// <summary>
    /// Optionally pass a list of enemies to ignore
    /// </summary>
    /// <param name="gosToIgnore"></param>
    /// <returns></returns>
    public GameObject NearestEnemy(List<GameObject> gosToIgnore = null)
    {
        float dist = Mathf.Infinity;
        if (oPool.activePool.Count < 1) return Camera.main.gameObject;
        if (gosToIgnore != null)
        {
            for (int i = 0; i < oPool.activePool.Count; i++)//there may be a more efficient way (2b optimized)
            {
                if (gosToIgnore.Contains(oPool.activePool[i])) continue;
                Vector2 directionToTarget = oPool.activePool[i].transform.position - gameObject.transform.position;
                float dSqrTT = directionToTarget.sqrMagnitude;
                if (dSqrTT < dist)
                {
                    dist = dSqrTT;
                    nearestEnemy = oPool.activePool[i];
                }
            }
            return nearestEnemy;
        }
        
        for (int i = 0; i < oPool.activePool.Count; i++)//there may be a more efficient way (2b optimized)
        {
            Vector2 directionToTarget = oPool.activePool[i].transform.position - gameObject.transform.position;
            float dSqrTT = directionToTarget.sqrMagnitude;
            if (dSqrTT< dist)
            {
                dist = dSqrTT;
                nearestEnemy = oPool.activePool[i];
            }
        }
        return nearestEnemy;
    }
}
