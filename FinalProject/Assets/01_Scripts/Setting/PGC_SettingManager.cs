using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : Singleton_DontDestroy<SettingManager> 
{
    [SerializeField]
    List<GameObject> allTitlePopup; //Start, Setting, KeySetting

    [SerializeField]
    List<GameObject> allLobyPopup; //Main, Chapter

    void Start()
    {
        SetPopup();
    }

    public void SetPopup()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            DisableAllPopups(allTitlePopup);

            allTitlePopup[0].SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Lobby")
        {
            DisableAllPopups(allLobyPopup);

            allLobyPopup[0].SetActive(true);
        }
    }

    public void StartButton()
    {
        SceneLoadManager.Instance.Load(KindOfScene.Lobby);
    }

    public void GameStartButton()
    {
        SceneLoadManager.Instance.Load(KindOfScene.Game);
    }

    public void SettingButton()
    {
        DisableAllPopups(allTitlePopup);

        allTitlePopup[1].SetActive(true);
    }

    public void GameExitButton()
    {
        DataManager.Instance.Save();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }

    public void InChapterButton()
    {
        //√©≈Õ »≠∏È
        DisableAllPopups(allLobyPopup);

        allLobyPopup[1].SetActive(true);
    }

    void DisableAllPopups(List<GameObject> allPopup)
    {
        for (int i = 0; i < allPopup.Count; i++)
        {
            allPopup[i].SetActive(false);
        }
    }
}
