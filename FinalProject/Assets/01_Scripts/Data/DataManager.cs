using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    const string AssetBundleCache = "AssetBundles";
    const string PlayerDataRoot = "PlayerData";
    const string SpriteStorageRoot = "Sprites";
    
    DatabaseReference dbReference;
    StorageReference storageReference;
    PlayerData playerData;
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();
    [SerializeField]
    List<string> assetBundleNames = new List<string>();
    string uuid;
#if UNITY_EDITOR
    int spriteID;
    [SerializeField]
    bool createTableFile;
#endif

    public PlayerData PlayerData => playerData;
    public bool LoadCompleted { get; private set; }

    protected override void OnAwake()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        storageReference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://garden-c0326.appspot.com/");
        uuid = SystemInfo.deviceUniqueIdentifier;

        Load();
        LoadSprites();
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
        var path = Path.Combine(Application.persistentDataPath, AssetBundleCache);
        DirectoryInfo directoryInfo = new DirectoryInfo(path);

        if (directoryInfo.Exists)
        {
            var files = directoryInfo.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name.Split(".").Length > 1)
                {
                    continue;
                }
                
                var filePath = Path.Combine(Application.persistentDataPath, AssetBundleCache, files[i].Name);
                
                var bundle = AssetBundle.LoadFromFile(filePath);
                var spriteArr = bundle.LoadAllAssets<Sprite>();

                sprites.AddRange(spriteArr);
            }
            Debug.Log("cache load success");
        }
        else
        {
            directoryInfo.Create();
            StartCoroutine(GetSpritesFromStorage());
#if UNITY_EDITOR
            CreateAssetBundleIndexTable();
#endif
            Debug.Log("File cache success");
        }
    }

    IEnumerator GetSpritesFromStorage()
    {
        var spriteRef = storageReference.Child(SpriteStorageRoot);

        for (int i = 0; i < assetBundleNames.Count; i++)
        {
            var fileName = assetBundleNames[i];
            Uri uri = null;

            spriteRef.Child(fileName).GetDownloadUrlAsync().ContinueWith(task => { uri = task.Result; });

            while (uri == null) yield return null;

            var request = UnityWebRequest.Get(uri);
            var path = Path.Combine(Application.persistentDataPath, AssetBundleCache, fileName);
            
            yield return request.SendWebRequest();

            File.WriteAllBytes(path, request.downloadHandler.data);

            if (fileName.Split(".").Length == 1)
            {
                var bundle = AssetBundle.LoadFromFile(path);
                var spriteArr = bundle.LoadAllAssets<Sprite>();
                sprites.AddRange(spriteArr);
            }
        }
    }

    void CreateAssetBundleIndexTable()
    {
        var path = Path.Combine(Application.dataPath, "SpriteIDTable.txt");

        StreamWriter streamWriter = new StreamWriter(path, File.Exists(path));

        for (int i = 0; i < sprites.Count; i++)
        {
            streamWriter.WriteLine(string.Format("{0} : {1}", spriteID, sprites[i].name));
            spriteID++;
            Debug.Log(sprites[i].name);
        }

        streamWriter.Close();
    }
}
