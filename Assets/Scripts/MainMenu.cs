using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject level2Warning;
    [SerializeField] GameObject level3Warning;
    [SerializeField] GameObject level4Warning;
    [SerializeField] GameObject gameComplete;
    [SerializeField] GameObject gameSaved;
    [SerializeField] GameObject gameLoaded;

    PersistentLogic gameLogic;

    private void Start()
    {
        gameLogic = FindObjectOfType<PersistentLogic>();
    }

    //If all levels have been completed a message will be dispalyed to the user.
    private void Update()
    {
        if( gameLogic.getLevel4Complete() == true)
        {
            gameComplete.SetActive(true);
        }
    }

    // Script provides the functionality for the overworld, detecting which level the user wishes to play through the collision volumes.
    // Handles stopping the user from entering levels that they have not unlocked.

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Level 1 Collider"))
        {
            SceneManager.LoadScene("Level 1 - Introduction");
        }
        else if (other.gameObject.CompareTag("Level 2 Collider"))
        {
            if (gameLogic.getLevel2Status() == true){
                SceneManager.LoadScene("Level 2 - Puzzles");
            }
            else
            {
                level2Warning.SetActive(true);
                Invoke("Reset2Warning", 5f);
            }
        }
        else if (other.gameObject.CompareTag("Level 3 Collider"))
        {
            if(gameLogic.getLevel3Status() == true)
            {
                SceneManager.LoadScene("Level 3 - Bouncing");
            }
            else
            {
                level3Warning.SetActive(true);
                Invoke("Reset3Warning", 5f);
            }
            
        }else if( other.gameObject.CompareTag("Level 4 Collider"))
        {
            if(gameLogic.getLevel4Status() == true)
            {
                SceneManager.LoadScene("Level 4 - Horde");
            }
            else
            {
                level4Warning.SetActive(true);
                Invoke("Reset4Warning", 5f);
            }
        }

        if (other.gameObject.CompareTag("SaveCollider"))
        {
            gameLogic.SaveData();
            gameSaved.SetActive(true);
            Invoke("ResetSave", 5f);
        }else if (other.gameObject.CompareTag("LoadCollider"))
        {
            gameLogic.LoadData();
            gameLoaded.SetActive(true);
            Invoke("ResetLoaded", 5f);
        }
    }

    // Functions to reset the UI warning elements that alert a user when they have not unlocked a level.
    void Reset2Warning()
    {
        level2Warning.SetActive(false);
    }

    void Reset3Warning()
    {
        level3Warning.SetActive(false);
    }

    void Reset4Warning()
    {
        level4Warning.SetActive(false);
    }

    void ResetSave()
    {
        gameSaved.SetActive(false);
    }

    void ResetLoaded()
    {
        gameLoaded.SetActive(false);
    }
}
