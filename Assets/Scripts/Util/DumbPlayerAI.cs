using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using System.Linq;
using System.IO;

public class DumbPlayerAI : MonoBehaviour
{
    /*TODO add session timer and session counts and decide on a max session length (maybe
     * 1.5 minutes? Also, add a restart session and make sure restarting scene works (it 
     * currently breaks) add tracking to abilities used, upgrades chosen, and death
     * )*/
    private string logFilePath;
    PlayerController pc;
    bool isMoving=false;
    float movingLength;
    int[] dirArr = { 0, 1, -1 };
    float abilityAttemptTimer = 0f;
    AbilityManager aM;
    private void Awake()
    {
        logFilePath = Path.Combine(Application.persistentDataPath, "crash_log.txt");
        DontDestroyOnLoad(gameObject);
        Application.logMessageReceived += OnLog;
    }
    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        aM = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<AbilityManager>();
    }
    private void OnDestroy()
    {
        Application.logMessageReceived -= OnLog;
    }
    void OnLog(string condition, string stackTrace, LogType type)
    {
        if (condition.Contains("[DumbAI]"))
        {
            File.AppendAllText(logFilePath, $"{condition}\n");
        }
        if (type == LogType.Exception || type == LogType.Error)
        {
            File.AppendAllText(logFilePath, $"[SESSION] ERROR: {condition}\n{stackTrace}\n\n");
        }
    }
    void Update()
    {
        MoveCharacter();
        UseAbility();
    }
    private void OnEnable()
    {
        UpgradeManager.ChooseUpgrade += ChooseUpgrade;
    }
    private void OnDisable()
    {
        UpgradeManager.ChooseUpgrade -= ChooseUpgrade;
    }
    private void MoveCharacter()
    {
        //remember to turn off any conflicting functions in other scripts
        if (!isMoving) {//get new direction and length of movement
            movingLength = Random.Range(0.5f, 5f);
            pc.dir = GetDirection();
            isMoving = true;
        }
        if (movingLength <= 0f) isMoving = false;
        movingLength -= Time.deltaTime;
    }
    private Vector2 GetDirection()
    {
        Vector2 dir = new Vector2();
        //roll for -1,0,1
        int newX=Random.Range(0, 3);
        int newY = Random.Range(0, 3);
        dir.x = dirArr[newX];
        dir.y = dirArr[newY];
        dir.Normalize();
        return dir;
    }
    private void UseAbility()
    {
        //remember to turn off any conflicting functions in other scripts
        if (abilityAttemptTimer >= .5f)
        {
            abilityAttemptTimer = 0f;
            RollAbility();
        }
        abilityAttemptTimer += Time.deltaTime;
    }
    private void RollAbility()
    {
        int roll = Random.Range(0, 4);
        TriggerAbility(roll);
    }
    private void TriggerAbility(int x)
    {
        Debug.Log($"[DumbAI] Attempting abiltiy {x} at Position {pc.transform.position} at Time: {Time.time} ");
        if (x == 0)
        {
            aM.Ability1();
        }else if (x == 1)
        {
            aM.Ability2();
        }else if (x == 2)
        {
            aM.Ability3();
        }
        else
        {
            if (aM.ability4 != null)
            {
                aM.Ability4();
            }
        }
    }
    private void ChooseUpgrade()
    {
        //remember to turn off any conflicting functions in other scripts
        //UpgradeManager uM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeManager>();
        UIManager uIM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        int i=uIM.upgradeButtonList.Count(b => b.gameObject.activeInHierarchy);//only get count of active buttons

        int roll = Random.Range(0,i);
        Debug.Log($"[DumbAI] Attempting to take upgrade {roll} at Position {pc.transform.position} at Time: {Time.time} ");
        uIM.upgradeButtonList[roll].onClick.Invoke();
    }
}
