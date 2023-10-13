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
    public List<bool> unlockedChapters;
    public SerializableDateTime recentPlayDateTime;
}

[Serializable]
public class StructureInfo
{
    public int index;
    public string name;
    public string structureSize;
    public int unlockLevel;
    public int price_Solar;
    public int price_Sun;
    public int upgradePrice_Solar;
    public List<bool> canCreate = new List<bool>();

    public StructureInfo(int index, string name, string structureSize, int unlockLevel, int priceSolar, int priceSun, int upgradePriceSolar, List<bool> canCreate)
    {
        this.index = index;
        this.name = name;
        this.structureSize = structureSize;
        this.unlockLevel = unlockLevel;
        this.price_Solar = priceSolar;
        this.price_Sun = priceSun;
        this.upgradePrice_Solar = upgradePriceSolar;

        this.canCreate = canCreate;
    }
}

[Serializable]
public class ItemInfo
{
    public int index;
    public string name;
    public int exp;
    public int price_Solar;
    public int craftTime;

    public ItemInfo(int index, string name, int exp, int priceSolar, int craftTime)
    {
        this.index = index;
        this.name = name;
        this.exp = exp;
        this.price_Solar = priceSolar;
        this.craftTime = craftTime;
    }
}

[Serializable]
public class LevelUpCost
{
    public uint level;
    public uint cost;
}