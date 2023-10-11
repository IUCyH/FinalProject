using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ItemDataTable
{
    static List<ItemInfo> table = new List<ItemInfo>();
    
    public static int Size => table.Count;
    
    public static ItemInfo GetInfo(int level)
    {
        return table[level];
    }

    static ItemDataTable()
    {
        var path = Application.persistentDataPath + @"\DataResult\ItemData";

        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        var files = directoryInfo.GetFiles();

        for (int i = 0; i < files.Length; i++)
        {
            StreamReader streamReader = new StreamReader(string.Format(@"{0}/{1:00}.txt", path, i));
            List<string> dataOfString = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                if (line == null) break;
                dataOfString.Add(line);
            }

            int.TryParse(dataOfString[0], out int index);
            string name = dataOfString[1];
            int.TryParse(dataOfString[2], out int exp);
            int.TryParse(dataOfString[3], out int priceSolar);
            int.TryParse(dataOfString[4], out int craftTime);

            ItemInfo itemInfo = new ItemInfo(index, name, exp, priceSolar, craftTime);
            table.Add(itemInfo);
        }
    }
}
