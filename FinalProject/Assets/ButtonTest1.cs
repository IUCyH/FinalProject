using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTest1 : MonoBehaviour
{
    public void GameStartButton()
    {
        SceneLoadManager.Instance.Load(KindOfScene.Game);
    }

    public void GoGardenButton()
    {
        SceneManager.LoadScene("DemoScene_Garden");
    }
}
