using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SearchSpriteName : EditorWindow
{
    static GUIContent title = new GUIContent("스프라이트 이름 검색");
    static List<string> searchableFolders = new List<string>();
    static List<string> resultList = new List<string>();
    static string userInput = "";
    
    [MenuItem("Search/Search Sprite Name")]
    static void OpenSearchWindow()
    {
        var window = GetWindow<SearchSpriteName>();

        window.titleContent = title;
        
        if (searchableFolders.Count < 1)
        {
           InitSearchableFolders(); 
        }
    }

    static void InitSearchableFolders()
    {
        var path = Path.Combine(Application.dataPath, "06_TextFiles", "SearchableFolder.txt");
        using (StreamReader streamReader = new StreamReader(path))
        {
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                if (line == null) break;
                
                searchableFolders.Add(line);
            }
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        GUILayout.Label("태그(관련된 주제) 또는 이름으로 검색 가능");
        
        if (e.isKey && e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)
        {
            Search();
        }
        userInput = GUILayout.TextField(userInput);
        if (GUILayout.Button("검색"))
        {
            Search();
        }

        if (!string.IsNullOrEmpty(userInput))
        {
            Search();
            for (int i = 0; i < resultList.Count; i++)
            {
                GUILayout.TextArea(resultList[i]);
            }
        }
    }

    static void Search()
    {
        if (string.IsNullOrEmpty(userInput)) return;
        
        resultList.Clear();

        for (int i = 0; i < searchableFolders.Count; i++)
        {
            var path = Path.Combine(Application.dataPath, "02_Sprites", searchableFolders[i]);
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            var directories = directoryInfo.GetDirectories();
            for (int j = 0; j < directories.Length; j++)
            {
                var files = directories[j].GetFiles();

                for (int k = 0; k < files.Length; k++)
                {
                    var fileName = files[k].Name;
                    var upper = userInput.ToUpper();
                    var lower = userInput.ToLower();
                    
                    if (fileName.Contains(userInput, StringComparison.OrdinalIgnoreCase) || fileName.Contains(userInput) || fileName.Contains(upper) || fileName.Contains(lower))
                    {
                        if(fileName.Contains(".meta")) continue;
                        
                        resultList.Add(fileName);
                    }
                }
            }
        }
    }
}
