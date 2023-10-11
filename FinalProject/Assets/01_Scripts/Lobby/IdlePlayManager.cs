using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class IdlePlayManager : Singleton<IdlePlayManager>
{
    TimeSpan timeSpan;
    float deltaSecond;
    [SerializeField]
    int rewardsPaymentTime = 2;

    protected override void OnAwake()
    {
        var lastPlayTime = DataManager.Instance.PlayerData.lastPlayTime;
        
        timeSpan = DateTime.Now - lastPlayTime;
        PaymentManager.Instance.Sell(KindOfGoods.IdleRewards, (int)timeSpan.TotalSeconds / 60 * 20);
    }

    void Update()
    {
        deltaSecond += Time.deltaTime;
        if ((int)deltaSecond >= rewardsPaymentTime)
        {
            PaymentManager.Instance.Sell(KindOfGoods.IdleRewards);
            deltaSecond = 0f;
        }
    }
}
