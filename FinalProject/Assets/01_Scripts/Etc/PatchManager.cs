using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Unity.VisualScripting;
using UnityEngine;

public class PatchManager : Singleton<PatchManager>
{
    const string AssetBundles = "AssetBundles";
    const string ManifestFile = "ManifestFile";
    
    DatabaseReference dbReference;
    List<string> filesShouldPatch = new List<string>();

    protected override void OnAwake()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async Task CheckUpdateAndPatch(DirectoryInfo directoryInfo, string defaultPath)
    {
        var files = directoryInfo.GetFiles();
        var count = files.Length;

        for (int i = 0; i < count; i++)
        {
            if (files[i].Name.Contains("manifest"))
            {
                await Patch(defaultPath, files[i].Name);
            }
        }

        await PatchNewFile(files);
    }

    async Task Patch(string defaultPath, string fileName)
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

    async Task PatchNewFile(FileInfo[] files)
    {
        var assetBundles = await dbReference.Child(AssetBundles).GetValueAsync();
        
        if (assetBundles.Exists)
        {
            filesShouldPatch.Clear();
            foreach (var child in assetBundles.Children)
            {
                if (child.Key == ManifestFile) continue;

                var value = (string)child.Value;

                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Name.Contains("manifest")) continue;

                    var fileName = files[i].Name;
                    if (fileName == value)
                    {
                        if (filesShouldPatch.Contains(value))
                        {
                            filesShouldPatch.Remove(value);
                        }

                        break;
                    }

                    if (!filesShouldPatch.Contains(value))
                    {
                        filesShouldPatch.Add(value);
                    }
                }
            }

            for (int i = 0; i < filesShouldPatch.Count; i++)
            {
                DataManager.Instance.DownloadNewFile(filesShouldPatch[i]);
                DataManager.Instance.DownloadNewFile(string.Format("{0}.{1}", filesShouldPatch[i], "manifest"));
            }
        }
    }
}
