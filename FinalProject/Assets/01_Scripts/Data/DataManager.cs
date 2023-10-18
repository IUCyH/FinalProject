using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Google.MiniJSON;
using TMPro;
using UnityEngine;

public class DataManager : Singleton_DontDestroy<DataManager>
{
    const string PlayerDataRoot = "PlayerData";
    
    DatabaseReference dbReference;
    PlayerData playerData;
    string uuid;

    public PlayerData PlayerData => playerData;
    public bool LoadCompleted { get; private set; }

    protected override void OnAwake()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
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
                    playerData = JsonUtility.FromJson<PlayerData>(snapshot.GetRawJsonValue());
                    playerData.unlockedChapters = JsonUtility.FromJson<List<bool>>(playerData.jsonOfChapters);
                }
                else
                {
                    playerData = new PlayerData
                    {
                        level = 0,
                        solarCoin = 0,
                        sunCoin = 0,
                        unlockedChapters = new List<bool>(),
                        recentPlayDateTime = new SerializableDateTime(DateTime.Now)
                    };
                    
                    Save();
                }

                Debug.Log("데이터 로드 성공");

                LoadCompleted = true;
            }
        });
    }
    
    public void Save()
    {
        playerData.jsonOfChapters = JsonUtility.ToJson(playerData.unlockedChapters);
        var json = JsonUtility.ToJson(playerData);
        
        dbReference.Child(PlayerDataRoot).Child(uuid).SetRawJsonValueAsync(json).ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("데이터 저장 성공");
            }
        });
    }
}
