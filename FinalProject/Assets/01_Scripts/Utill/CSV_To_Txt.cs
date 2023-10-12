using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Database;
using UnityEditor;
using UnityEngine;

enum KindOfConvert
{
    RemoveData = -1,
    Structure,
    Item,
    LevelUpCost
}

public class CSV_To_Data : EditorWindow
{
    static GUIContent titleOfRemoveData = new GUIContent("Remove Data");
    static GUIContent titleOfConvertStructure = new GUIContent("Convert To StructureData");
    static GUIContent titleOfConvertItem = new GUIContent("Convert To ItemData");
    static GUIContent titleOfLevelUpCostData = new GUIContent("Convert To LevelUpCostData");

    string path;
    string dbPath;

    float centerX;
    float buttonWidth = 250f;
    float buttonHeight = 30f;

    static KindOfConvert kindOfConvert;
    
    [MenuItem("Data/RemoveData")]
    static void OpenRemoveDataWindow()
    {
        var window = GetWindow<CSV_To_Data>();

        window.titleContent = titleOfRemoveData;
        kindOfConvert = KindOfConvert.RemoveData;
    }

    [MenuItem("Data/Convert To StructureData")]
    static void OpenConvertToStructureDataWindow()
    {
        var window = GetWindow<CSV_To_Data>();

        window.titleContent = titleOfConvertStructure;
        kindOfConvert = KindOfConvert.Structure;
    }

    [MenuItem("Data/Convert To ItemData")]
    static void OpenConvertToItemDataWindow()
    {
        var window = GetWindow<CSV_To_Data>();

        window.titleContent = titleOfConvertItem;
        kindOfConvert = KindOfConvert.Item;
    }
    
    [MenuItem("Data/Convert To LevelUpCostData")]
    static void OpenConvertToLevelUpCostDataWindow()
    {
        var window = GetWindow<CSV_To_Data>();

        window.titleContent = titleOfLevelUpCostData;
        kindOfConvert = KindOfConvert.LevelUpCost;
    }

    void ConvertAndSaveFile()
    {
        StreamReader streamReader = new StreamReader(path);
        var firebaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line == null) break;

            var result = line.Split(",");
            var index = result[0];
            try
            {
                firebaseReference.Child("DataTable").Child(dbPath).Child(index).SetValueAsync(line);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        streamReader.Close();
        Debug.Log("데이터 저장 성공");
    }

    void OnGUI()
    {
        if (kindOfConvert == KindOfConvert.RemoveData)
        {
            GUILayout.Label("삭제할 데이터 테이블의 루트 이름 입력");
            path = GUILayout.TextField(path);
            centerX = (Screen.width - buttonWidth) / 2;
            
            GUILayout.BeginArea(new Rect(new Vector2(centerX, 50f), new Vector2(buttonWidth, buttonHeight)));
            
            if (GUILayout.Button("삭제하기"))
            {
                if (!string.IsNullOrEmpty(path))
                {
                    var dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                    var root = dbReference.Child("DataTable").Child(path);

                    root.RemoveValueAsync().ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Debug.Log("데이터 삭제 실패");
                        }
                        
                        else if (task.IsCompleted)
                        {
                            Debug.Log("데이터 삭제 성공");
                        }
                    });
                }
            }
            
            GUILayout.EndArea();
            
            return;
        }
        
        centerX = (Screen.width - buttonWidth) / 2;
        
        GUILayout.Label("CSV 파일의 경로 입력");
        path = GUILayout.TextField(path);

        GUILayout.BeginArea(new Rect(new Vector2(centerX, 50f), new Vector2(buttonWidth, buttonHeight)));

        if (kindOfConvert == KindOfConvert.Structure)
        {
            dbPath = "Structure";
        }
        else if (kindOfConvert == KindOfConvert.Item)
        {
            dbPath = "Item";
        }
        else if (kindOfConvert == KindOfConvert.LevelUpCost)
        {
            dbPath = "LevelUpCost";
        }

        if (GUILayout.Button("저장하기"))
        {
            if (!string.IsNullOrEmpty(path))
            {
                ConvertAndSaveFile();
            }
        }

        GUILayout.EndArea();
    }
}
