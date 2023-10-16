using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum KindOfScene
{
    None = -1,
    Title,
    Lobby,
    Game
}

public class SceneLoadManager : Singleton_DontDestroy<SceneLoadManager>
{
    AsyncOperation loadingInfo;
    [SerializeField]
    Sprite progressbar;
    [SerializeField]
    Sprite progressbarBG;

    KindOfScene currLoadScene = KindOfScene.None;

    public KindOfScene CurrentScene => currLoadScene;

    public void Load(KindOfScene scene)
    {
        loadingInfo = SceneManager.LoadSceneAsync((int)scene);
        currLoadScene = scene;
        ProgressBarManager.Instance.ShowLoadingWindow();
    }

    void Update()
    {
        if (loadingInfo != null && currLoadScene != KindOfScene.None)
        {
            if (loadingInfo.isDone)
            {
                ProgressBarManager.Instance.UpdateProgressBar(progressbarBG, progressbar, 1f);
                ProgressBarManager.Instance.HideLoadingWindow();
            }
            else
            {
                ProgressBarManager.Instance.UpdateProgressBar(progressbarBG, progressbar, loadingInfo.progress);
            }
        }     
    }
}
