using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentLogic : MonoBehaviour
{
    //Script manages the persistent states of the game throughout its playtime
    //For now it only manages which levels the user has done as well as which levels should be unlocked
    //Also manages how data is loaded when a save game is loaded from the main menu

    private static bool created = false;

    public bool level2Unlocked = false;
    public bool level3Unlocked = false;
    public bool level4Unlocked = false;
    public bool level4Complete = false;

    private void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    public void setLevel2Status()
    {
        level2Unlocked = true;
    }

    public bool getLevel2Status()
    {
        return level2Unlocked;
    }

    public void setLevel3Status()
    {
        level3Unlocked = true;
    }

    public bool getLevel3Status()
    {
        return level3Unlocked;
    }

    public void setLevel4Status()
    {
        level4Unlocked = true;
    }

    public bool getLevel4Status()
    {
        return level4Unlocked;
    }

    public void setLevel4Complete()
    {
        level4Complete = true;
    }

    public bool getLevel4Complete()
    {
        return level4Complete;
    }

    public void SaveData()
    {
        SaveSystem.SaveLevels(this);
    }

    public void LoadData()
    {
        LevelData data = SaveSystem.loadLevels();

        level2Unlocked = data.level2Unlocked;
        level3Unlocked = data.level3Unlocked;
        level4Unlocked = data.level4Unlocked;
        level4Complete = data.level4Complete;
    }

}
