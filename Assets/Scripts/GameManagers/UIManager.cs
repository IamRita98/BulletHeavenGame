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
    public List<Button> upgradeButtonList;
    int buttonCount = 0;

    void Start()
    {
        playerBStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseWeaponStats = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BaseWeaponStats>();
        upgradeManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UpgradeManager>();
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
