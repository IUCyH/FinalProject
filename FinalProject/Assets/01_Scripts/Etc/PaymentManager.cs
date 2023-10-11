using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KindOfGoods
{
    IdleRewards,
    Max
}

public class PaymentManager : Singleton_DontDestroy<PaymentManager>
{
    List<int> rewardsList = new List<int> { 20 };
    List<int> priceList;

    public void Sell(KindOfGoods goods, int gold = -1)
    {
        var playerData = DataManager.Instance.PlayerData;
        int reward = gold;
        
        if (gold < 0)
        {
            reward = rewardsList[(int)goods];
        }

        playerData.gold += reward;
    }

    public void Buy(KindOfGoods goods)
    {
        var playerData = DataManager.Instance.PlayerData;

        playerData.gold -= priceList[(int)goods];
    }
}
