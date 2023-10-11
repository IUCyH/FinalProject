using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StructureItemDataTable
{
    static List<StructureInfo> table = new List<StructureInfo>();

    public static List<StructureInfo> Table => table;

    static StructureItemDataTable()
    {
        var path = PlayerPrefs.GetString("StreamWriterPath", string.Empty);

        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        var files = directoryInfo.GetFiles();

        for (int i = 0; i < files.Length; i++)
        {
            StreamReader streamReader = new StreamReader(string.Format(@"{0}/{1:0}단계.txt", path, i + 1));
            List<string> dataOfString = new List<string>();
            
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();

                if (line == null) break;
                dataOfString.Add(line);
            }

            int.TryParse(dataOfString[0], out int index);
            string name = dataOfString[1];
            string structureSize = dataOfString[2];
            int.TryParse(dataOfString[3], out int unlockLevel);
            int.TryParse(dataOfString[4], out int priceSolar);
            int.TryParse(dataOfString[5], out int priceSun);
            int.TryParse(dataOfString[6], out int upgradePriceSolar);

            List<bool> canCreate = new List<bool>();
            for (int j = 7; j < dataOfString.Count; j++)
            {
                canCreate.Add(int.Parse(dataOfString[j]) == 1);
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
        }
    }
}
