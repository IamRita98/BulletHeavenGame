using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoLogging : MonoBehaviour
{
    // Start is called before the first frame update
    public static AutoLogging Instance;
    private string logFilePath;
    Button startButton;
    Button unitButton;
    List<Button> mmButtons;
    private int session = 0;
    private float curSessionTimer = 0f;
    float maxSessionLength = 60f;//
    GameStateManager gsm;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        string date = System.DateTime.Now.ToString("MMMM_d");
        logFilePath = Path.Combine(Application.persistentDataPath, $"{date}_crash_log.txt");
        DontDestroyOnLoad(gameObject);
        Application.logMessageReceived += OnLog;
    }
    private void Start()
    {
        gsm = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<GameStateManager>();
        FindButtons();
        TryStartTest();
    }
    private void Update()
    {
        curSessionTimer += Time.deltaTime;
        if (curSessionTimer >= maxSessionLength)
        {
            //new session
            Debug.Log($"Ending [DumbAI] session {session}---starting next session");
            RestartSession();
        }
    }
    private void OnEnable()
    {
        CombatHandler.OnPlayerDeath += RestartSession;
    }
    private void OnDisable()
    {
        CombatHandler.OnPlayerDeath -= RestartSession;
    }
    private void RestartSession()
    {
        if (gsm == null)
        {
            gsm = GameObject.FindGameObjectWithTag("PersistentManager").GetComponent<GameStateManager>();
        }
        gsm.testingSession = true;
        gsm.UnPauseGame();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            BaseStats pS = player.GetComponent<BaseStats>();
            pS.Health.AddFlatValue(pS.MaxHealth.StatsValue());
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("MainMenu");
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
       
        FindButtons();
        TryStartTest();

    }
    private void FindButtons()
    {
        startButton = GameObject.Find("Start").GetComponent<Button>();
        mmButtons = Resources.FindObjectsOfTypeAll<Button>().ToList();
        //Transform pCanvas = GameObject.Find("Canvas").transform;
        //unitButton = GameObject.Find("DefaultDaniel").GetComponent<Button>(true);
        unitButton = mmButtons.FirstOrDefault(b => b.name == "DefaultDaniel");
    }
    private void TryStartTest()
    {
        session++;
        curSessionTimer = 0f;
        startButton.onClick.Invoke();
        unitButton.onClick.Invoke();
        Time.timeScale = 2f;
    }
    private void OnDestroy()
    {
        Debug.Log($"---[DumbAI] Ending this test session on session {session}---");
        Application.logMessageReceived -= OnLog;
        CombatHandler.OnPlayerDeath -= RestartSession;
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
}
