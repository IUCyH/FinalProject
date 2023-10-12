using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Database;
using UnityEditor;
using UnityEngine;

enum KindOfConvert
{
    Structure,
    Item,
    LevelUpCost
}

public class UploadData : EditorWindow
{
    static GUIContent titleOfConvertStructure = new GUIContent("구조물 데이터 파일 업로드");
    static GUIContent titleOfConvertItem = new GUIContent("아이템 데이터 파일 업로드");
    static GUIContent titleOfLevelUpCostData = new GUIContent("레벨업 비용 데이터 파일 업로드");

    string path;
    string dbPath;

    float centerX;
    float buttonWidth = 250f;
    float buttonHeight = 30f;

    static KindOfConvert kindOfConvert;

    [MenuItem("Data/구조물 데이터 파일 업로드")]
    static void OpenUploadStructureDataWindow()
    {
        var window = GetWindow<UploadData>();

        window.titleContent = titleOfConvertStructure;
        kindOfConvert = KindOfConvert.Structure;
    }

    [MenuItem("Data/아이템 데이터 파일 업로드")]
    static void OpenUploadItemDataWindow()
    {
        var window = GetWindow<UploadData>();

        window.titleContent = titleOfConvertItem;
        kindOfConvert = KindOfConvert.Item;
    }
    
    [MenuItem("Data/레벨업 비용 데이터 파일 업로드")]
    static void OpenUploadLevelUpCostDataWindow()
    {
        var window = GetWindow<UploadData>();

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
            firebaseReference.Child("DataTable").Child(dbPath).Child(index).SetValueAsync(line).ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("데이터 저장 성공");
                }
                else
                {
                    Debug.LogError("데이터 저장 실패");
                }
            });
        }

        streamReader.Close();
    }

    void OnGUI()
    {
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
