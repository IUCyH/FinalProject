using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using UnityEngine;

public static class LevelUpCostDataTable
{
    static List<LevelUpCost> table = new List<LevelUpCost>();

    public static int Size => table.Count;
    public static bool DataLoadCompleted { get; private set; }

    public static LevelUpCost GetInfo(int level)
    {
        return table[level];
    }

    static LevelUpCostDataTable()
    {
        var dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        var root = dbReference.Child("DataTable").Child("LevelUpCost");
        root.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                var snapshot = task.Result;

                foreach (var data in snapshot.Children)
                {
                    string value = (string)data.Value;
                    var result = value.Split(",");

                    uint.TryParse(result[0], out uint level);
                    uint.TryParse(result[1], out uint cost);

                    LevelUpCost levelUpCost = new LevelUpCost
                    {
                        level = level,
                        cost = cost
                    };
                    
                    table.Add(levelUpCost);
                }

                table = table.OrderBy(cost => cost.level).ToList();
                DataLoadCompleted = true;
            }
        });
    }
}
