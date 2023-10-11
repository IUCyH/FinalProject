using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Scene
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

    Scene currLoadScene = Scene.None;

    public void Load(Scene scene)
    {
        loadingInfo = SceneManager.LoadSceneAsync((int)scene);
        currLoadScene = scene;
        ProgressBarManager.Instance.ShowLoadingWindow();
    }

    void Update()
    {
        if (loadingInfo != null && currLoadScene != Scene.None)
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
