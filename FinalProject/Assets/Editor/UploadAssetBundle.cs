using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Firebase.Database;
using Firebase.Storage;
using UnityEditor;
using UnityEngine;

public class UploadAssetBundle : EditorWindow
{
    const string SpriteStorageRoot = "Sprites";
    
    static StorageReference storageReference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://garden-c0326.appspot.com/");
    static DatabaseReference firebaseDatabase = FirebaseDatabase.DefaultInstance.RootReference;
    
    [MenuItem("AssetBundle/Upload AssetBundles")]
    static async void Upload()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(Application.dataPath, "05_AssetBundles"));
        var spriteRef = storageReference.Child(SpriteStorageRoot);
        var files = directoryInfo.GetFiles();
        var fileCount = files.Length;

        for (int i = 0; i < fileCount; i++)
        {
            var path = Path.Combine(Application.dataPath, "05_AssetBundles", files[i].Name);
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var name = files[i].Name;

            var splitResult = name.Split(".");

            if(splitResult.Length > 1)
            {
                var length = splitResult.Length;
                if (length == 2 ? splitResult[1] == "meta" : splitResult[2] == "meta") //에셋번들과 manifest 파일의 meta 파일 제외
                {
                    continue;
                }
            }
            
            await spriteRef.Child(name).PutStreamAsync(stream);
            stream.Close();
            
            if (splitResult.Length == 1 || name.Contains("manifest")) //만약 에셋번들이거나 메니페스트 파일이라면(에셋번들은 확장자가 없으므로 splitResult의 length가 1이다.)
            {
               SaveNameOfAssetBundleAndManifest(name, splitResult.Length == 1 ? name : "ManifestFile", splitResult[0], path);
            }
        }
    }

    static async void SaveNameOfAssetBundleAndManifest(string dataName, string childName, string manifestFileName, string path)
    {
        var streamReader = new StreamReader(path);
        var child = firebaseDatabase.Child("AssetBundles").Child(childName);
                
        if (dataName.Contains("manifest"))
        {
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line.Contains("CRC"))
                {
                    var crc = line.Split(":")[1];
                    crc.Trim();
                            
                    dataName += "," + crc;
                    child = child.Child(manifestFileName);
                }
            }
        }
                
        await child.SetValueAsync(dataName);
        streamReader.Close();
    }
}
