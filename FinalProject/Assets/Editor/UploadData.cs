using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase.Database;
using UnityEditor;
using UnityEngine;

public class UploadData : EditorWindow
{
    static GUIContent titleOfUpload = new GUIContent("데이터 파일 DB Table로 업로드");

    string path;
    string dbPath;

    [MenuItem("Data/데이터 파일 DB Table로 업로드")]
    static void OpenUploadStructureDataWindow()
    {
        var window = GetWindow<UploadData>();

        window.titleContent = titleOfUpload;
    }

    void UploadFile()
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
        GUILayout.Label("1. 데이터 테이블 이름 입력");
        dbPath = GUILayout.TextField(dbPath);
        
        GUILayout.Label("2. CSV 파일의 경로 입력");
        path = GUILayout.TextField(path);
        
        if (GUILayout.Button("저장하기"))
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(dbPath))
            {
                UploadFile();
            }
        }
    }
}
