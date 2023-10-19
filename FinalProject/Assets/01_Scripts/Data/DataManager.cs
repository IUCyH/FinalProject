using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Storage;
using Google.MiniJSON;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DataManager : Singleton_DontDestroy<DataManager>
{
    const string PlayerDataRoot = "PlayerData";
    const string SpriteStorageRoot = "Sprites";
    
    DatabaseReference dbReference;
    StorageReference storageReference;
    PlayerData playerData;
    string uuid;

    public PlayerData PlayerData => playerData;
    public bool LoadCompleted { get; private set; }

    protected override void OnAwake()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        storageReference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://garden-c0326.appspot.com/");
        uuid = SystemInfo.deviceUniqueIdentifier;

        Load();
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
    
    void Load()
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
                }

                Save();
                Debug.Log("데이터 로드 성공");

                LoadCompleted = true;
            }
        });
    }

    void LoadSprites()
    {
        var spritesRef = storageReference.Child(SpriteStorageRoot);

        /*spritesRef.GetStreamAsync().ContinueWith(stream =>
        {
            if (stream.IsCompleted)
            {
                var result = stream.Result;
                var assetBundle = AssetBundle.LoadFromStreamAsync(result);
                
            }
        });*/
    }
}
