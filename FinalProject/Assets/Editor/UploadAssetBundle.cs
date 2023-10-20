using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Storage;
using UnityEditor;
using UnityEngine;

public class UploadAssetBundle : EditorWindow
{
    const string SpriteStorageRoot = "Sprites";
    
    static StorageReference storageReference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://garden-c0326.appspot.com/");
    
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
            if (splitResult.Length > 1)
            {
                var length = splitResult.Length;
                if (length == 2 ? splitResult[1] == "meta" : splitResult[2] == "meta")
                {
                    continue;
                }
            }
            
            await spriteRef.Child("/" + name).PutStreamAsync(stream);
            
            stream.Close();
        }
    }
}
