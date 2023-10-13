using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using UnityEngine;

public static class MaxStoreCapacityOfSolarCoinDataTable
{
    static List<MaxStoreCapacityOfSolarCoin> table = new List<MaxStoreCapacityOfSolarCoin>();

    public static int Size => table.Count;
    public static bool DataLoadCompleted { get; private set; }
    
    public static MaxStoreCapacityOfSolarCoin GetInfo(int index)
    {
        return table[index];
    }

    static MaxStoreCapacityOfSolarCoinDataTable()
    {
        var dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        var root = dbReference.Child("DataTable").Child("MaxCapacityOfSolar");

        root.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                var snapshot = task.Result;

                foreach (var data in snapshot.Children)
                {
                    var value = (string)data.Value;
                    var result = value.Split(",");
                    
                    int.TryParse(result[0], out int level);
                    float.TryParse(result[1], out float normal);
                    float.TryParse(result[2], out float highClass);
                    float.TryParse(result[3], out float rare);
                    float.TryParse(result[4], out float hero);
                    float.TryParse(result[5], out float legend);

                    MaxStoreCapacityOfSolarCoin maxStoreCapacityOfSolarCoin = new MaxStoreCapacityOfSolarCoin
                    {
                        level = level,
                        normal = normal,
                        highClass = highClass,
                        rare = rare,
                        hero = hero,
                        legend = legend
                    };

                    table.Add(maxStoreCapacityOfSolarCoin);
                }

                table = table.OrderBy(capacity => capacity.level).ToList();
                for (int i = 0; i < table.Count; i++)
                {
                    Debug.Log(table[i].level);
                    Debug.Log(table[i].normal);
                    Debug.Log(table[i].highClass);
                    Debug.Log("////////////////////");
                }
                DataLoadCompleted = true;
            }
        });
    }
}
