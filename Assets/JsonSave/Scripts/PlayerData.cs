using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : Singleton<PlayerData>{

    [System.Serializable]
    public class Player
    {
        public bool firsTime;
    }

    public Player player = new Player();

    public bool FirstTime
    {
        get
        {
            LoadPlayer();
            return player.firsTime;
        }
        set
        {
            player.firsTime = value;
            SavePlayer();
        }
    }

    private void LoadPlayer()
    {
        string dataPath;
        string jsonString;

        dataPath = Application.persistentDataPath + "/player.json";

        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            jsonString = streamReader.ReadToEnd();
            player = JsonUtility.FromJson<Player>(jsonString);
            streamReader.Close();
        }

        //Debug.Log("" + dataPath);
    }

    private void SavePlayer()
    {
        string dataPath;
        string jsonString;

        dataPath = Application.persistentDataPath + "/player.json";
       
        using (StreamWriter streamWriter = File.CreateText(dataPath))
        {
            jsonString = JsonUtility.ToJson(player);
            streamWriter.Write(jsonString);
            streamWriter.Close();
        }
    }
}

