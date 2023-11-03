using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CILoad : Singleton<CILoad>
{
    protected override void OnStart()
    {
        StartCoroutine(Coroutine_LoadToTitle());
    }

    IEnumerator Coroutine_LoadToTitle()
    {
        for (int i = 0; i < 600; i++) yield return null;
        
        SceneLoadManager.Instance.Load(KindOfScene.Title);
    }
}
