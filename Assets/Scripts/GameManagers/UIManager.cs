using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    BaseStats playerBStats;
    BaseWeaponStats baseWeaponStats;
    UpgradeManager upgradeManager;
    LevelUpManager levelUpManager;
    AbilityManager abilityManager;
    public List<Button> upgradeButtonList;
    public TMP_Text playerHp;
    public TMP_Text playerXp;
    public TMP_Text ability1CD;
    public TMP_Text ability2CD;
    public TMP_Text ability3CD;

    //We should consider making an event for each ability activation. This would let us MakeStats in the ability only when
    //it's used (and probably fixing the bug of dmg not being applied to beam). Alternatively we could go w/ the idea of
    //sending an event on levelup to recheck stats. Signal here would let us send other specific info to the abilities
    //in case we even wanted to do anything w/ that too.
    
    void Start()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
        upgradeManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeManager>();
        levelUpManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelUpManager>();
        abilityManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AbilityManager>();
    }

    private void Update()
    {
        playerHp.text = (playerBStats.Health.StatsValue() + "/" + playerBStats.MaxHealth.StatsValue() + "HP");
        playerXp.text = (playerBStats.XP.StatsValue() + "/" + levelUpManager.XPThreshold + "XP");
        ability1CD.text = (abilityManager.ability1Timer.ToString("0.##") + "s");
        ability2CD.text = (abilityManager.ability2Timer + "s");
        //ability3CD.text = (abilityManager.ability3Timer + "s");
    }

    public void DisplayUpgrade(UpgradeManager.UpgradeInfo upgradeInfo,int upgradeButton, UpgradeManager.UpgradeTypes upgradeType)
    {
        Time.timeScale = 0f;
        Button tempButton = upgradeButtonList[upgradeButton];
        tempButton.gameObject.SetActive(true);
        TMP_Text buttonText = tempButton.GetComponentInChildren<TMP_Text>();
        int tier = upgradeInfo.GetTier();
        string textToDisplay;
        textToDisplay = upgradeInfo.descriptions[tier];
        buttonText.text = textToDisplay;
        tempButton.onClick.AddListener(delegate { upgradeManager.ApplyUpgrades(tier, upgradeType);});
    }

    public void HideUpgrades()
    {
        foreach(Button btn in upgradeButtonList)
        {
            btn.gameObject.SetActive(false);
        }
        Time.timeScale = 1f;
    }
}
