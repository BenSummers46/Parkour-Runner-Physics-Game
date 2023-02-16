using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //Uses a binary formatter to save the current data to a binary file.
    public static void SaveLevels (PersistentLogic pl)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/level.data";

        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(pl);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    //Once again uses a binary formatter to load the users data from the file saved before.
    public static LevelData loadLevels()
    {
        string path = Application.persistentDataPath + "/level.data";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save File does not exist");
            return null;
        }
    }

}
