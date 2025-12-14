using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBuffing : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// generic script to buff a specific stat for specific type
    /// </summary>
    /// <param name="GO">an object to be buffed</param>
    /// <param name="amount">amount to buff by</param>
    /// <param name="buffType">type (ie: weapon or ability)</param>
    /// <param name="buff">the specific stat to buff</param>
    public void BuffStat(GameObject GO,float amount, string buffType,string buff)
    {
        if (buffType == "global")
        {

        }else if (buffType == "weapon")
        {
            if (buff.Contains("damage"))
            {
                GO.GetComponent<BaseWeaponStats>().BaseDamage.AddMultiValue(amount);
            }else if (buff.Contains("projectile"))
            {
                GO.GetComponent<BaseWeaponStats>().Projectiles.AddFlatValue(amount);
            }else if (buff.Contains("pierce"))
            {
                GO.GetComponent<BaseWeaponStats>().Pierce.AddFlatValue(amount);
            }else if (buff.Contains("rate"))
            {
                GO.GetComponent<BaseWeaponStats>().AttackRate.AddMultiValue(amount);
            }
            else if (buff.Contains("area"))
            {
                GO.GetComponent<BaseWeaponStats>().WeapArea.AddMultiValue(amount);
            }
            else
            {
                GO.GetComponent<BaseWeaponStats>().LifeTime.AddMultiValue(amount);
            }
        }
        else//always will be abilities
        {

        }
    }
}
