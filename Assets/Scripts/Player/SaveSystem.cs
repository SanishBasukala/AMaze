using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayer(PlayerHealth playerHealth)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/Player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerHealth);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("file not found: " + path);
            return null;
        }
    }
}
