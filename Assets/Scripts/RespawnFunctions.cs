using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnFunctions : MonoBehaviour
{
    // Controls the menu functions for the level completed menu as well as the death menu.

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
    
}
