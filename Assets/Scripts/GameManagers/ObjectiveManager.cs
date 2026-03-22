using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;

public class ObjectiveManager : MonoBehaviour
{
    public TMP_Text enemiesKilled;
    public TMP_Text objectiveProgression;
    public TMP_Text currentObjective;
    public GameObject movePlayer;
    //public TMP_Text objective3;
    public int enemyDeathsTracker = 0;
    GameObject objectiveWall;
    private string objective2 = "Enter door to final area";
    private string objective3 = "Kill zone boss";
    private string enemiesKilledString="Enemies Killed: ";
    private void Awake()
    {
        objectiveWall = GameObject.FindGameObjectWithTag("ObjectiveWall");
    }
    private void Start()
    {
        enemiesKilled.text = enemiesKilledString+ enemyDeathsTracker.ToString();
    }
    private void OnEnable()
    {
        CombatHandler.OnEnemyDeath += EnemyDeathTracking;
    }
    private void OnDisable()
    {
        CombatHandler.OnEnemyDeath -= EnemyDeathTracking;
    }
    // need to add something for boss
    private void EnemyDeathTracking(GameObject gameObject)
    {
        enemyDeathsTracker++;
        enemiesKilled.text =enemiesKilledString+ enemyDeathsTracker.ToString();
        if (enemyDeathsTracker >= 100)
        {
            StrikeThroughObjective(currentObjective);
            Destroy(objectiveWall);//disbale objective wall
            objectiveProgression.text = "2/3";
            currentObjective.text = objective2;
            movePlayer.SetActive(true);
        }

        
    }
    public void MoveObjectiveComplete()
    {
        StrikeThroughObjective(currentObjective);
        currentObjective.text = objective3;
    }
    private void StrikeThroughObjective(TMP_Text objectiveText)
    {
        string temp = objectiveText.text;
        objectiveText.text=("<s>"+temp+"<s>");
    }
}
