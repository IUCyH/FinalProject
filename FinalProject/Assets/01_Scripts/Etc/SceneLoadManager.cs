using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum KindOfScene
{
    None = -1,
    CI,
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
        loadingInfo = SceneManager.LoadSceneAsync((int)KindOfScene.Loading);
        currLoadScene = scene;
        loadingCanvas.enabled = true;
        progressbar.fillAmount = 0f;
        //ProgressBarManager.Instance.ShowLoadingWindow();
    }

    void Update()
    {
        if (currLoadScene != KindOfScene.None)
        {
            var progress = loadingInfo.progress * DataManager.Instance.LoadProgress;
            progressbar.fillAmount = progress;

            if (progress >= 0.9f)
            {
                loadingTimer += Time.deltaTime;

                progressbar.fillAmount = Mathf.Lerp(progressbar.fillAmount, 1f, loadingTimer);

                if (progressbar.fillAmount >= 1f)
                {
                    SceneManager.LoadSceneAsync((int)currLoadScene);
                    progressbar.fillAmount = 1f;
                    currLoadScene = KindOfScene.None;
                    loadingCanvas.enabled = false;
                }
            }
        }
    }
}
