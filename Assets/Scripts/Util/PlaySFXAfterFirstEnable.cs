using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFXAfterFirstEnable : MonoBehaviour
{
    public string key;
    FMOD.Studio.EventInstance sfxToPlay;

    int timesThisHasBeenCalled = -1;

    // Start is called before the first frame update
    void Start()
    {
        sfxToPlay = RuntimeManager.CreateInstance(key);
    }

    public void playSFX()
    {
        timesThisHasBeenCalled++;
        if(timesThisHasBeenCalled >= 0)
        {
            sfxToPlay.start();
        }
    }
}
