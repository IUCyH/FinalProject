using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

enum KindOfConvert
{
    Structure,
    Item
}

public class CSV_To_Data : EditorWindow
{
    static GUIContent titleOfConvertStructure = new GUIContent("Convert To StructureData");
    static GUIContent titleOfConvertItem = new GUIContent("Convert To ItemData");

    string path;
    string streamWriterPath;

    float centerX;
    float buttonWidth = 250f;
    float buttonHeight = 30f;

    static KindOfConvert kindOfConvert;

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

    void ConvertToStructureData()
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line == null) break;
            
            var result = line.Split(",");
            
            string name = result[1].Split(" ")[0];
            
            StreamWriter streamWriter = new StreamWriter(string.Format(@"{0}\{1}.txt", streamWriterPath, name));

            for (int i = 0; i < result.Length; i++)
            {
                streamWriter.WriteLine(result[i]);
            }
            streamWriter.Close();
        }

        streamReader.Close();
    }

    void ConvertToItemData()
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line == null) break;

            var result = line.Split(",");

            /*
            int index = int.Parse(result[0]);
            string name = result[1];

            int exp = 0;
            int price_Solar = 0;
            int craftTime = 0;
            if (result[2] != "-") exp = int.Parse(result[2]);
            if (result[3] != "-") price_Solar = int.Parse(result[3]);
            if (result[4] != "-") craftTime = int.Parse(result[4]);

            ItemInfo itemInfo = new ItemInfo(index, name, exp, price_Solar, craftTime);
            
            ItemDataTable.table.Add(itemInfo);
            
            Debug.Log(itemInfo.index);
            Debug.Log(itemInfo.name);
            Debug.Log(itemInfo.exp);
            Debug.Log(itemInfo.price_Solar);
            Debug.Log(itemInfo.craftTime);
            Debug.Log("====================================================");*/
        }
        
        streamReader.Close();
    }

    void OnGUI()
    {
        centerX = (Screen.width - buttonWidth) / 2;

        GUILayout.Label("결과물을 저장할 폴더의 경로 입력");
        streamWriterPath = GUILayout.TextField(streamWriterPath);
        
        PlayerPrefs.SetString("StreamWriterPath", streamWriterPath);
        PlayerPrefs.Save();
        
        GUILayout.Label("CSV 파일의 경로 입력");
        path = GUILayout.TextField(path, GUILayout.Height(300f));

        GUILayout.BeginArea(new Rect(new Vector2(centerX, 450f), new Vector2(buttonWidth, buttonHeight)));

        if (kindOfConvert == KindOfConvert.Structure)
        {
            if (GUILayout.Button("구조물 데이터로 저장하기", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                if (!string.IsNullOrEmpty(path))
                {
                    ConvertToStructureData();
                }
            }
        }
        else if (kindOfConvert == KindOfConvert.Item)
        {
            if (GUILayout.Button("아이템 데이터로 저장하기", GUILayout.Width(buttonWidth), GUILayout.Height(buttonHeight)))
            {
                if (!string.IsNullOrEmpty(path))
                {
                    ConvertToItemData();
                }
            }
        }

        GUILayout.EndArea();
    }
}
