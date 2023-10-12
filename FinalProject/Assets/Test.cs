using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coroutine_DebugData());
    }

    IEnumerator Coroutine_DebugData()
    {
        while (!StructureItemDataTable.DataLoadCompleted) yield return null;
        
        for (int i = 0; i < StructureItemDataTable.Size; i++)
        {
            var data = StructureItemDataTable.GetInfo(i);
            
            Debug.Log(data.index);
            Debug.Log(data.name);
            Debug.Log(data.structureSize);
            Debug.Log(data.unlockLevel);
            Debug.Log(data.price_Solar);
            Debug.Log(data.price_Sun);
            Debug.Log(data.upgradePrice_Solar);

            for (int j = 0; j < data.canCreate.Count; j++)
            {
                Debug.Log(data.canCreate[j]);
            }
            
            Debug.Log("///////////////////////////////////////////////////////");
        }

        while (!ItemDataTable.DataLoadCompleted) yield return null;
        
        Debug.Log("===========================================");

        for (int i = 0; i < ItemDataTable.Size; i++)
        {
            var data = ItemDataTable.GetInfo(i);
            
            Debug.Log(data.index);
            Debug.Log(data.name);
            Debug.Log(data.exp);
            Debug.Log(data.price_Solar);
            Debug.Log(data.craftTime);
            Debug.Log("///////////////////////////////////////////////////");
        }

        while (!LevelUpCostDataTable.DataLoadCompleted) yield return null;
        
        Debug.Log("===========================================");

        for (int i = 0; i < LevelUpCostDataTable.Size; i++)
        {
            var data = LevelUpCostDataTable.GetInfo(i);
            
            Debug.Log(data.level);
            Debug.Log(data.cost);
            Debug.Log("///////////////////////////////////////////////////");
        }
    }
}
