using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEditor;
using UnityEngine;

public class RemoveDataTable : EditorWindow
{
    static GUIContent title = new GUIContent("데이터 테이블 삭제");

    string root;
    
    [MenuItem("DataTable/데이터 테이블 삭제")]
    static void OpenRemoveDataTableWindow()
    {
        var window = GetWindow<RemoveDataTable>();

        window.titleContent = title;
    }

    void OnGUI()
    {
        GUILayout.Label("삭제하려는 데이터 테이블 입력");
        root = GUILayout.TextField(root);

        if (GUILayout.Button("삭제하기"))
        {
            if (string.IsNullOrEmpty(root)) return;
            
            var dbReference = FirebaseDatabase.DefaultInstance.RootReference;

            dbReference.Child("DataTable").Child(root).RemoveValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    Debug.Log("데이터 삭제 성공");
                }
                else
                {
                    Debug.LogError("데이터 삭제 실패");
                }
            });
        }
    }
}
