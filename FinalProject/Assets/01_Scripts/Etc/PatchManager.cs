using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Database;
using UnityEngine;

public class PatchManager : Singleton<PatchManager>
{
    const string AssetBundles = "AssetBundles";
    const string ManifestFile = "ManifestFile";
    
    DatabaseReference dbReference;

    protected override void OnAwake()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void CheckUpdateAndPatch(DirectoryInfo directoryInfo, string defaultPath)
    {
        var files = directoryInfo.GetFiles();
        var count = files.Length;

        for (int i = 0; i < count; i++)
        {
            if (files[i].Name.Contains("manifest"))
            {
                Patch(defaultPath, files[i].Name);
            }
        }
    }

    async void Patch(string defaultPath, string fileName)
    {
        var manifestFilePath = Path.Combine(defaultPath, fileName);
        StreamReader streamReader = new StreamReader(manifestFilePath);

        while (!streamReader.EndOfStream)
        {
            var line = await streamReader.ReadLineAsync();

            if (line == null) break;
                    
            if (line.Contains("CRC"))
            {
                var snapshot = await dbReference.Child(AssetBundles).Child(ManifestFile).GetValueAsync();

                if (snapshot.Exists)
                {
                    var fileNameSplit = fileName.Split(".");
                    var shouldPatch = CompareOldFileAndNewFile(snapshot, fileNameSplit[0], line);

                    if (shouldPatch)
                    {
                        DataManager.Instance.DownloadNewFile(fileNameSplit[0]);
                        DataManager.Instance.DownloadNewFile(fileName);
                    }
                }
                        
                break;
            }
        }
                
        streamReader.Close();
    }

    bool CompareOldFileAndNewFile(DataSnapshot snapshot, string fileNameSplitResult, string line)
    {
        var data = snapshot.Child(fileNameSplitResult);
        var result = (string)data.Value;
                            
        var oldDataSplit = result.Split(",");
        var newDataSplit = line.Split(':');
                            
        if (!string.Equals(newDataSplit[1], oldDataSplit[1]))
        {
            return true;
        }

        return false;
    }
}
