using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject defaultDanielGO;
    Vector2 spawnPosOffScreen = new Vector2(100, 0);

    public void DefaultDanielSelected()
    {
        Instantiate(defaultDanielGO, spawnPosOffScreen, Quaternion.identity);
    }
}
