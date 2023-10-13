using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Firebase.Database;
using UnityEngine;

public static class ItemDataTable
{
    static List<ItemInfo> table = new List<ItemInfo>();

    public static int Size => table.Count;
    public static bool DataLoadCompleted { get; private set; }

    public static ItemInfo GetInfo(int index)
    {
        return table[index];
    }

    static ItemDataTable()
    {
        var dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        var root = dbReference.Child("DataTable").Child("Item");
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
                    int.TryParse(result[2], out int exp);
                    int.TryParse(result[3], out int priceSolar);
                    int.TryParse(result[4], out int craftTime);

                    ItemInfo itemInfo = new ItemInfo
                    (
                        index,
                        name,
                        exp,
                        priceSolar,
                        craftTime
                    );
                    table.Add(itemInfo);
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
