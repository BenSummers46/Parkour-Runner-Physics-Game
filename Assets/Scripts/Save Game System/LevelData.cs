using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Stores the level data that will be saved and loaded by the user.

[System.Serializable]
public class LevelData
{
    public bool level2Unlocked;
    public bool level3Unlocked;
    public bool level4Unlocked;
    public bool level4Complete;

    public LevelData (PersistentLogic pl)
    {
        level2Unlocked = pl.level2Unlocked;
        level3Unlocked = pl.level3Unlocked;
        level4Unlocked = pl.level4Unlocked;
        level4Complete = pl.level4Complete;
    }
}
