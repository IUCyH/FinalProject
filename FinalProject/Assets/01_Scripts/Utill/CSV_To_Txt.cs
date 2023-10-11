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

    int orderOfData;
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

    void ConvertAndSaveFile()
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (line == null) break;
            
            var result = line.Split(",");
            
            

            /*StreamWriter streamWriter = new StreamWriter(string.Format(@"{0}\{1:00}.txt", streamWriterPath, orderOfData++));

            for (int i = 0; i < result.Length; i++)
            {
                streamWriter.WriteLine(result[i]);
            }
            streamWriter.Close();*/
        }

        streamReader.Close();
    }

    void OnGUI()
    {
        orderOfData = 0;
        
        centerX = (Screen.width - buttonWidth) / 2;
        
        GUILayout.Label("CSV 파일의 경로 입력");
        path = GUILayout.TextField(path);

        GUILayout.BeginArea(new Rect(new Vector2(centerX, 50f), new Vector2(buttonWidth, buttonHeight)));

        if (kindOfConvert == KindOfConvert.Structure)
        {
            streamWriterPath = Application.persistentDataPath + @"\DataResult\StructureData";
        }
        else if (kindOfConvert == KindOfConvert.Item)
        {
            streamWriterPath = Application.persistentDataPath + @"\DataResult\ItemData";
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
