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
    Canvas loadingCanvas;
    [SerializeField]
    Image loadingBar;
    
    Scene currLoadScene = Scene.None;

    protected override void OnStart()
    {
        loadingCanvas.enabled = false;
    }

    public void Load(Scene scene)
    {
        loadingInfo = SceneManager.LoadSceneAsync((int)scene);
        currLoadScene = scene;

        loadingCanvas.enabled = true;
    }

    void Update()
    {
        if (loadingInfo != null && currLoadScene != Scene.None)
        {
            if (loadingInfo.isDone)
            {
                loadingBar.fillAmount = 1f;
                currLoadScene = Scene.None;
                
                loadingCanvas.enabled = false;
            }
            else
            {
                loadingBar.fillAmount = loadingInfo.progress;
            }
        }
    }
}
