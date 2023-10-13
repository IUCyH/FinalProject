using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllPopupButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> allTitlePopup; //Start, Setting, KeySetting

    [SerializeField]
    List<GameObject> allLobyPopup; //Main, Chapter

    [SerializeField]
    ChapterScroll chapterScroll;

    void Start()
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
        SceneLoadManager.Instance.Load(Scene.Lobby);
    }  

    public void SettingButton()
    {
        DisableAllPopups(allTitlePopup);

        allTitlePopup[1].SetActive(true);
    }   

    public void GameExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void KeySettingButton()
    {
        DisableAllPopups(allTitlePopup);

        allTitlePopup[2].SetActive(true);
    }

    public void InChapterButton()
    {
        DisableAllPopups(allLobyPopup);

        allLobyPopup[1].SetActive(true);

        chapterScroll.enabled = true;
    }

    public void OutChapterButton()
    {
        DisableAllPopups(allLobyPopup);

        allLobyPopup[0].SetActive(true);

        chapterScroll.enabled = false;
    }

    void DisableAllPopups(List<GameObject> allPopup)
    {
        for (int i = 0; i < allPopup.Count; i++)
        {
            allPopup[i].SetActive(false);
        }
    }
}
