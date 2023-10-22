using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class RegisterFolder : EditorWindow
{
    static GUIContent title = new GUIContent("검색가능한 폴더 등록");
    string folderName;
    
    [MenuItem("Search/Register Searchable Folder")]
    static void OpenRegisterWindow()
    {
        var window = GetWindow<RegisterFolder>();

        window.titleContent = title;
    }

    void OnGUI()
    {
        GUILayout.Label("등록할 폴더 이름 입력");
        folderName = GUILayout.TextField(folderName);

        if (GUILayout.Button("등록") && !string.IsNullOrEmpty(folderName))
        {
            var path = Path.Combine(Application.dataPath, "06_TextFiles", "SearchableFolder.txt");
            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.WriteLine(folderName);
            }
        }
    }
}
