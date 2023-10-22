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
    string userInput;
    
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
        
        if (e.isKey && e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.Return)
            {
                //TODO : 검색 로직 실행
            }
        }
        userInput = GUILayout.TextField(userInput);
        if (GUILayout.Button("검색"))
        {
            //TODO : 검색 로직 실행
        }
    }
}
