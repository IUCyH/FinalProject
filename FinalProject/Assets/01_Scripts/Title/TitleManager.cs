using System.Collections;
using System.Collections.Generic;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class TitleManager : Singleton<TitleManager>
{
    [SerializeField]
    NetworkRunner networkRunner;
    
    protected override void OnAwake()
    {
        networkRunner.ProvideInput = true;

        networkRunner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.AutoHostOrClient,
            SessionName = "TestSession",
            Scene = (int)SceneLoadManager.Instance.CurrentScene
        });
    }
}
