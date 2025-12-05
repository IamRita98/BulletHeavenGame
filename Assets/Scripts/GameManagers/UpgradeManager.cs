using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Button WeapDamageButton;
    int weaponDamageTier = 0;
    int healthTier = 0;
    int movementSpeedTier = 0;
    int fireRateTier = 0;
    int allAbilityDamageTier = 0;
    //int ability1DamageTier = 0;
    int globalDamageTier = 0;

  Dictionary<string,> upgradeDictionary=new Dictionary<string, int>{   
        {"weaponDamage",0},
        {"health",0}
    };
    
    private void Awake()
    {
        WeapDamageButton.onClick.AddListener(WeaponDamageUpgrade());
    }
    void WeaponDamageUpgrade()
    {

    }
    public void RollUpgrades()
    {
        int possibleUpgrades = 3;//for now
        //int rarity = Random.Range(0, 4);
        for (int i = 0; i < possibleUpgrades; i++)
        {
            int upgradeChosen = Random.Range(0, 4);
            switch (upgradeChosen)
            {
                case 0://weapon
                    UpgradeTier("weaponDamage",upgradeDictionary["weaponDamage"]);
                    break;
            }

        }
    }

    void UpgradeTier(string key,int tier)
    {
        if (tier >= 3)
        {
            //remove from available upgrades list
        }
        else
        {
            upgradeDictionary[key] += 1;
        }
            
    }
    
}
