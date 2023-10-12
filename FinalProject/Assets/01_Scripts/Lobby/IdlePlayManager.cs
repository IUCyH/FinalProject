using System;
using UnityEngine;

public class IdlePlayManager : Singleton<IdlePlayManager>
{
    TimeSpan timeSpan;
    float deltaSecond;
    [SerializeField]
    int rewardsPaymentTime = 2;

    protected override void OnAwake()
    {
        var lastPlayTime = DataManager.Instance.PlayerData.recentPlayDateTime;
        
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
