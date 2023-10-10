using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton_DontDestroy<DataManager>
{
    const string PlayerDataKey = "PlayerData";

    PlayerData playerData;

    public void Load()
    {
        var jsonOfPlayer = PlayerPrefs.GetString(PlayerDataKey, string.Empty);

        if (string.IsNullOrEmpty(jsonOfPlayer))
        {
            playerData = new PlayerData();
        }
        else
        {
            playerData = JsonUtility.FromJson<PlayerData>(jsonOfPlayer);
        }

        Save();
    }
    
    public void Save()
    {
        var jsonOfPlayer = JsonUtility.ToJson(playerData);
        
        PlayerPrefs.SetString(jsonOfPlayer, PlayerDataKey);

        PlayerPrefs.Save();
    }
}
