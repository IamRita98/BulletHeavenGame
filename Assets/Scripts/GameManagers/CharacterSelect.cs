using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject defaultDanielGO;
    public GameObject sarahSwordGO;
    Vector2 spawnPosOffScreen = new Vector2(100, 0);

    public void DefaultDanielSelected()
    {
        Instantiate(defaultDanielGO, spawnPosOffScreen, Quaternion.identity);
    }

    public void SarahSwordSelected()
    {
        Instantiate(sarahSwordGO, spawnPosOffScreen, Quaternion.identity);
    }
}
