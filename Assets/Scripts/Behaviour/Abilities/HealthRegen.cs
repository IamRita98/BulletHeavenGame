using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    /// <summary>
    /// health regen is tick based-> controlled by our regenValue (x/regenValue) each tick is +1hp
    /// </summary>
    float timer=0;
    //public float tickTimer = 1;
    public float regenValue=0;
    BaseStats bs;
    void Start()
    {
        bs=this.GetComponent<BaseStats>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= (2/regenValue)&&regenValue>0)
        {
            Regen();
            timer = 0;
        }
    }
    void Regen()
    {
        bs.Health.AddFlatValue(1f);
    }
}
