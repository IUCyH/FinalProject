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
        DataManager.Instance.PlayerData.gold += ((int)timeSpan.TotalSeconds / 60) * 20;
    }

    void Update()
    {
        deltaSecond += Time.deltaTime;
        if ((int)deltaSecond >= rewardsPaymentTime)
        {
            Debug.Log((int)deltaSecond / rewardsPaymentTime * 20);
            deltaSecond = 0f;
        }
    }
}
