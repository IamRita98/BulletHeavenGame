using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject charSelectUI;


    public void ChangeUIFromMainMenuToCharSelect()
    {
        mainMenuUI.SetActive(false);
        charSelectUI.SetActive(true);
    }

    public void ChangeUIFromCharSelectToMainMenu()
    {
        mainMenuUI.SetActive(true);
        charSelectUI.SetActive(false);
    }
}
