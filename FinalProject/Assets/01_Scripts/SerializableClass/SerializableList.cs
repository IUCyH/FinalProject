using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableList<T>
{
    List<T> list = new List<T>();
    [SerializeField]
    string jsonFile = " ";

    bool initialized;
    
    public T this[int index]
    {
        get
        {
            if (!initialized)
            {
                list = JsonUtility.FromJson<List<T>>(jsonFile);
                initialized = true;
            }
            
            return list[index];
        }
        
        set
        {
            if (!initialized)
            {
                list = JsonUtility.FromJson<List<T>>(jsonFile);
                initialized = true;
            }
            
            list[index] = value;
            jsonFile = JsonUtility.ToJson(list);
        }
    }

    public SerializableList(string json)
    {
        if (!string.IsNullOrEmpty(json))
        {
            //jsonFile = json;
            //list = JsonUtility.FromJson<List<T>>(json);

            initialized = true;
        }
    }

    public void Add(T item)
    {
        list.Add(item);
        jsonFile = JsonUtility.ToJson(list);
    }
}
