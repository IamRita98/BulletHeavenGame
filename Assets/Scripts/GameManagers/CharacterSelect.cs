using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject defaultDanielGO;
    public GameObject sarahSwordGO;
    Vector2 spawnPosOffScreen = new Vector2(100, 0);
    FMOD.Studio.EventInstance characterSelectedSFX;

    private void Start()
    {
        characterSelectedSFX = FMODUnity.RuntimeManager.CreateInstance("event:/Gun");
    }

    public void DefaultDanielSelected()
    {
        characterSelectedSFX.setParameterByName("Character Selected", 0f);
        Instantiate(defaultDanielGO, spawnPosOffScreen, Quaternion.identity);
    }

    public void SarahSwordSelected()
    {
        characterSelectedSFX.setParameterByName("Character Selected", 1f);
        Instantiate(sarahSwordGO, spawnPosOffScreen, Quaternion.identity);
    }
}
