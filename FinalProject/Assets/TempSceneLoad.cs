using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSceneLoad : MonoBehaviour
{
    void Awake()
    {
        if (!DataManager.Instance.LoadCompleted)
        {
            SceneLoadManager.Instance.Load(KindOfScene.Loading);
        }
    }
}
