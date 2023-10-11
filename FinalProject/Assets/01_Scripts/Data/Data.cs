using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int level;
    public int gold;
    public int diamond;
    public DateTime lastPlayTime;
}

[Serializable]
public class StructureInfo
{
    int index;
    string name;
    int unlockLevel;
    int price_Solar;
    int price_Sun;
    int upgradePrice_Solar;
    bool[] canCreate;
}