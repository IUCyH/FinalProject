using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
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

                string json = string.Empty;
                long ticks = DateTime.Now.Ticks;
                int level = 0;
                int gold = 0;
                int diamond = 0;
                SerializableList<bool> unlockedChapters = null;
                SerializableDateTime recentPlayDateTime = null;

                if (snapshot.Exists)
                {
                    json = snapshot.Child("unlockedChapters").Child("jsonFile").GetRawJsonValue();
                    ticks = (long)snapshot.Child("recentPlayDateTime").Child("ticks").Value;

                    level = int.Parse(snapshot.Child("level").GetRawJsonValue());
                    gold = int.Parse(snapshot.Child("gold").GetRawJsonValue());
                    diamond = int.Parse(snapshot.Child("diamond").GetRawJsonValue());
                }

                unlockedChapters = new SerializableList<bool>(json);
                recentPlayDateTime = new SerializableDateTime(ticks);
                
                playerData = new PlayerData
                {
                    level = level,
                    gold = gold,
                    diamond = diamond,
                    unlockedChapters = unlockedChapters,
                    recentPlayDateTime = recentPlayDateTime
                };

                Debug.Log("데이터 로드 성공");
                Save();

                LoadCompleted = true;
            }
        });
    }
    
    public void Save()
    {
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
