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
    Game,
    Loading
}

public class SceneLoadManager : Singleton_DontDestroy<SceneLoadManager>
{
    AsyncOperation loadingInfo;
    [SerializeField]
    Image progressbar;
    [SerializeField]
    Canvas loadingCanvas;

    float loadingTimer;

    KindOfScene currLoadScene = KindOfScene.None;

    public KindOfScene CurrentScene => currLoadScene;

    public void Load(KindOfScene scene)
    {
        SceneManager.LoadSceneAsync((int)KindOfScene.Loading);
        loadingInfo = SceneManager.LoadSceneAsync((int)scene);
        loadingInfo.allowSceneActivation = false;
        currLoadScene = scene;
        loadingCanvas.enabled = true;
        //ProgressBarManager.Instance.ShowLoadingWindow();
    }

    void Update()
    {
        if (loadingInfo != null)
        {
            if (loadingInfo.progress < 0.9f)
            {
                progressbar.fillAmount = loadingInfo.progress;
            }
            else
            {
                loadingTimer += Time.unscaledDeltaTime;
                progressbar.fillAmount = Mathf.Lerp(loadingInfo.progress, 1f, loadingTimer);

                if (progressbar.fillAmount >= 1f)
                {
                    progressbar.fillAmount = 0f;
                    loadingTimer = 0f;
                    loadingCanvas.enabled = false;
                    loadingInfo.allowSceneActivation = true;
                }
            }
        }
    }
}
