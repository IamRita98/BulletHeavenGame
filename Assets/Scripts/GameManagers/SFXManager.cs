using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
/*    /// <summary>
    /// Currently there is a bug where if a new SFX starts while another one is running, the first one will have it's volume changed
    /// to the volume of the new one. There may be a fix in code. Alternatively, we could do actual audio mixing, changing
    /// the volume for each method call is kind of jank. Wwise probably has a solution to this too...
    /// </summary>
    [Header("DanielSFX")]
    public AudioClip bulletSFX;
    public AudioClip beamSFX;
    public AudioClip buffFieldSFX;

    [Header("MonsterSFX")]
    public AudioClip MonsterDeathSFX;

    [Header("GenericSFX")]
    public AudioClip cdT3DingSFX;
    public AudioClip cdT3ICANTSTOPWINNINGSFX;
    public AudioClip reviveSFX;

    [Header("OtherSFX")]
    public AudioClip xpGemSFX;
    public AudioClip playerHurtSFX;

    AudioSource aSource;
    PlayerController playerController;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void PlayBulletSFX()
    {//Should consider making that a generic method for ProjWep to call then if/switching
     //to make the right sound based on the currently played character. Rn this is only DD
        aSource.volume = .06f;
        aSource.clip = bulletSFX;
        aSource.PlayOneShot(aSource.clip);

    }

    public void PlayBeamSFX()
    {
        aSource.volume = .55f;
        aSource.clip = beamSFX;
        aSource.PlayOneShot(aSource.clip);
    }

    public void PlayPlaceBuffFieldSFX()
    {
        aSource.volume = 1f;
        aSource.clip = buffFieldSFX;
        aSource.PlayOneShot(aSource.clip);
    }

    public void PlayMonsterDeathSFX()
    {
        aSource.volume = .5f;
        aSource.clip = MonsterDeathSFX;
        aSource.Play();
    }

    public void PlayCDT3DingSFX()
    {

    }

    public void PlayCDT3ICANTSTOPWINNINGSFX()
    {

    }

    public void PlayReviveSFX()
    {

    }

    public void PlayXPGemPickupSFX()
    {
        aSource.volume = .3f;
        aSource.clip = xpGemSFX;
        aSource.Play();
    }

    public void PlayPlayerHurtSFX()
    {
        aSource.volume = .9f;
        aSource.clip = playerHurtSFX;
        aSource.PlayOneShot(aSource.clip);
    }*/
}
