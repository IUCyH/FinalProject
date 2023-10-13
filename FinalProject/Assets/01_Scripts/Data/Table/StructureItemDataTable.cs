using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Firebase.Database;
using UnityEngine;

public static class StructureItemDataTable
{
    static List<StructureInfo> table = new List<StructureInfo>();

    public static int Size => table.Count;
    public static bool DataLoadCompleted { get; private set; }
    
    public static StructureInfo GetInfo(int index)
    {
        return table[index];
    }

    static StructureItemDataTable()
    {
        var dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        var root = dbReference.Child("DataTable").Child("Structure");
        root.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                var snapshot = task.Result;

                foreach (var data in snapshot.Children)
                {
                    string value = (string)data.Value;
                    var result = value.Split(",");
                    
                    int.TryParse(result[0], out int index);
                    string name = result[1];
                    string structureSize = result[2];
                    int.TryParse(result[3], out int unlockLevel);
                    int.TryParse(result[4], out int priceSolar);
                    int.TryParse(result[5], out int priceSun);
                    int.TryParse(result[6], out int upgradePriceSolar);
                    
                    List<bool> canCreate = new List<bool>();
                    for (int j = 7; j < result.Length; j++)
                    {
                        canCreate.Add(int.Parse(result[j]) == 1);
                    }
                    
                    StructureInfo structureInfo = new StructureInfo
                    (
                        index,
                        name,
                        structureSize,
                        unlockLevel,
                        priceSolar,
                        priceSun,
                        upgradePriceSolar,
                        canCreate
                    );
                    table.Add(structureInfo);
                    Debug.Log(index);
                    Debug.Log(name);
                    Debug.Log(priceSolar);
                }

                table = table.OrderBy(info => info.index).ToList();
                DataLoadCompleted = true;
            }
        });
    }
}
