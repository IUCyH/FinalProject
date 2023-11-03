using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempSceneLoad : Singleton_DontDestroy<TempSceneLoad>
{
    protected override void OnAwake()
    {
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(TestDataLoad());
    }

    IEnumerator TestDataLoad()
    {
        while (!DataManager.Instance.LoadCompleted) yield return null;

        SceneManager.LoadScene("Lobby");
    }
}
