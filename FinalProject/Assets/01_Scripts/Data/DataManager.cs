using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Storage;
using Google.MiniJSON;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class DataManager : Singleton_DontDestroy<DataManager>
{
    const string AssetBundles = "AssetBundles";
    const string PlayerDataRoot = "PlayerData";
    const string SpriteStorageRoot = "Sprites";
    const string ManifestFile = "ManifestFile";
    
    DatabaseReference dbReference;
    StorageReference storageReference;
    PlayerData playerData;
    List<string> bundleNames = new List<string>();
    string uuid;

    public PlayerData PlayerData => playerData;
    public bool LoadCompleted { get; private set; }

    protected override void OnAwake()
    {
        //TODO : 스프라이트 get 함수 구현
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        storageReference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://garden-c0326.appspot.com/");
        uuid = SystemInfo.deviceUniqueIdentifier;

        Load();
        LoadAssetBundles();
    }

    public void DownloadNewFile(string fileName)
    {
        StartCoroutine(Coroutine_CacheSpriteFromStorage(fileName));
    }
    
    IEnumerator Coroutine_CacheSpriteFromStorage(string fileName)
    {
        Uri uri = null;
        var fileRef = storageReference.Child(SpriteStorageRoot).Child(fileName);

        fileRef.GetDownloadUrlAsync().ContinueWith(task => { uri = task.Result; });
        while (uri == null) yield return null;
        
        var request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
        
        var path = Path.Combine(Application.persistentDataPath, AssetBundles, fileName);
        File.WriteAllBytes(path, request.downloadHandler.data);
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
            }
        });
    }

    async void LoadAssetBundles()
    {
        var path = Path.Combine(Application.persistentDataPath, AssetBundles);
        DirectoryInfo directoryInfo = new DirectoryInfo(path);

        await GetBundleNames();
        
        if(!directoryInfo.Exists)
        {
            directoryInfo.Create();
            StartCoroutine(Coroutine_CacheSpritesFromStorage());
        }
        else
        {
            PatchManager.Instance.CheckUpdateAndPatch(directoryInfo, path);
        }

        LoadCompleted = true;
    }
    
    async Task GetBundleNames()
    {
        var snapshot = await dbReference.Child(AssetBundles).GetValueAsync();
        
        if (snapshot.Exists)
        {
            foreach (var data in snapshot.Children)
            {
                if (data.Key == ManifestFile)
                {
                    GetManifestFileNames(data);
                    continue;
                }
                bundleNames.Add((string)data.Value);
            }
        }
    }

    void GetManifestFileNames(DataSnapshot data)
    {
        foreach (var child in data.Children)
        {
            var value = (string)child.Value;
            var split = value.Split(',');
            
            bundleNames.Add(split[0]);
        }
    }

    IEnumerator Coroutine_CacheSpritesFromStorage()
    {
        var spriteRef = storageReference.Child(SpriteStorageRoot);

        for (int i = 0; i < bundleNames.Count; i++)
        {
            var fileName = bundleNames[i];
            Uri uri = null;

            spriteRef.Child(fileName).GetDownloadUrlAsync().ContinueWith(task =>
            {
                uri = task.Result;
            });

            while (uri == null) yield return null;

            var request = UnityWebRequest.Get(uri);
            var path = Path.Combine(Application.persistentDataPath, AssetBundles, fileName);
            
            yield return request.SendWebRequest();

            File.WriteAllBytes(path, request.downloadHandler.data);
        }
    }
}
