using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class DataManager : Singleton_DontDestroy<DataManager>
{
    const string PlayerDataRoot = "PlayerData";
    
    DatabaseReference dbReference;
    PlayerData playerData;
    string uuid;

    public PlayerData PlayerData => playerData;

    protected override void OnAwake()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        playerData = new PlayerData();
        uuid = SystemInfo.deviceUniqueIdentifier;
        
        Load();
    }

    public void Load()
    {
        dbReference.Child(PlayerDataRoot).Child(uuid).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("데이터 로드 실패");
            }
            
            else if (task.IsCompleted)
            {
                var snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Debug.Log(snapshot);
                }
                else
                {
                    playerData = new PlayerData
                    {
                        level = 0,
                        gold = 0,
                        diamond = 0,
                        unlockedChapters = new List<bool>(),
                        recentPlayDateTime = new SerializableDateTime(DateTime.Now)
                    };
                    
                    Save();
                }
                
                Debug.Log("데이터 로드 성공");
            }
        });
    }
    
    public void Save()
    {
        playerData.recentPlayDateTime.DateTime = DateTime.Now;
        var json = JsonUtility.ToJson(playerData);
        
        dbReference.Child(PlayerDataRoot).Child(uuid).SetRawJsonValueAsync(json);
    }
}
