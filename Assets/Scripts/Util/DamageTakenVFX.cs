using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenVFX : MonoBehaviour
{
    //Color colorToFlashOnDamage = new Color(195, 79, 79, 255);
    //Color defaultColor = new Color(255,255,255, 255);
    float timeToChangeColor = .15f;
    public IEnumerator colorChangeCoroutine;
    SpriteRenderer sRend;

    private void Awake()
    {
        colorChangeCoroutine = ColorChange(null);
        sRend = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        sRend.color = Color.white;
    }

    public void DamageTaken(GameObject goHit)
    {
        StartCoroutine(ColorChange(sRend));
    }

    IEnumerator ColorChange(SpriteRenderer sRend)
    {
        sRend.color = Color.red;
        yield return new WaitForSeconds(timeToChangeColor);
        sRend.color = Color.white;
    }
}
