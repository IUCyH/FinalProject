using System;
using System.Collections;
using UnityEngine;

public class IdlePlayManager : Singleton<IdlePlayManager>
{
    SerializableDateTime lastPlayTime;
    
    TimeSpan timeSpan;
    float deltaSecond;
    [SerializeField]
    int rewardsPaymentTime = 2;

    bool dataLoaded;

    protected override void OnStart()
    {
        StartCoroutine(Coroutine_GiveRewards());
    }

    void Update()
    {
        if (!dataLoaded) return;
        
        deltaSecond += Time.deltaTime;
        if ((int)deltaSecond >= rewardsPaymentTime)
        {
            PaymentManager.Instance.Sell(KindOfGoods.IdleRewards);
            deltaSecond = 0f;
        }
    }

    IEnumerator Coroutine_GiveRewards()
    {
        while(!DataManager.Instance.LoadCompleted) yield return null;
        
        lastPlayTime = DataManager.Instance.PlayerData.recentPlayDateTime;
        timeSpan = DateTime.Now - lastPlayTime;
        
        PaymentManager.Instance.Sell(KindOfGoods.IdleRewards, (int)timeSpan.TotalSeconds / 60 * 20);

        dataLoaded = true;
    }
}
