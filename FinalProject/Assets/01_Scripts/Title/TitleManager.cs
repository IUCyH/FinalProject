using System.Collections;
using System.Collections.Generic;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class TitleManager : Singleton<TitleManager>
{
    protected override void OnStart()
    {
        StartCoroutine(Coroutine_SetPlayerOnline());
    }

    IEnumerator Coroutine_SetPlayerOnline()
    {
        while (!DataManager.Instance.LoadCompleted)
        {
            yield return null;
        }
        
        DataManager.Instance.SetPlayerOnline();
    }
}
