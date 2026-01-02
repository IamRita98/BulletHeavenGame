using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    BaseStats baseStats;
    public GameObject danielGO;
    public GameObject sarahSwordGO;
    private void Awake()
    {
        
    }
    public void DefaultDanielSelected()
    {
        //SceneManager.LoadScene("TestLevel");
        Instantiate(danielGO, Vector3.zero,Quaternion.identity);
    }
}
