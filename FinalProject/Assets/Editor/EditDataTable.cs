using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using UnityEditor;
using UnityEngine;

public class EditDataTable : EditorWindow
{
    static GUIContent titleOfEditTable = new GUIContent("테이블 수정");

    string root;
    string index;
    string data;

    [MenuItem("DataTable/데이터 테이블 수정")]
    static void OpenEditTableWindow()
    {
        var window = GetWindow<EditDataTable>();

        window.titleContent = titleOfEditTable;
    }

    void OnGUI()
    {
        GUILayout.Label("1. 수정하려는 데이터 테이블의 이름 입력");
        GUILayout.Label("(Item, LevelUpCost, Structure)");
        root = GUILayout.TextField(root);
        
        GUILayout.Label("2. 수정하려는 데이터의 인덱스 번호 입력");
        index = GUILayout.TextField(index);
        
        GUILayout.Label("3. 수정하려는 내용 포함 전체 데이터 입력");
        data = GUILayout.TextField(data);
        
        if (GUILayout.Button("수정하기"))
        {
            if (!string.IsNullOrEmpty(root) && !string.IsNullOrEmpty(index) && !string.IsNullOrEmpty(data))
            {
                var dbReference = FirebaseDatabase.DefaultInstance.RootReference;
                var root = dbReference.Child("DataTable").Child(this.root);

                root.Child(index).SetValueAsync(data).ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        Debug.Log("데이터 변경 성공");
                    }

                    else
                    {
                        Debug.LogError("데이터 변경 실패");
                    }
                });
            }
        }
    }
}
