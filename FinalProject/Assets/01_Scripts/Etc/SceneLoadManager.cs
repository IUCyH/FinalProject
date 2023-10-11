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

    Scene currLoadScene = Scene.None;

    public void Load(Scene scene)
    {
        loadingInfo = SceneManager.LoadSceneAsync((int)scene);
        currLoadScene = scene;
    }

    void Update()
    {
        //TODO : 로딩 바 업데이트 로직
    }
}
