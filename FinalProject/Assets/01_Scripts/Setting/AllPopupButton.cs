using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllPopupButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> allTitlePopup; //Start, Setting, KeySetting

    [SerializeField]
    List<GameObject> allGamePopup; //Main, Chapter

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            DisableAllPopups(allTitlePopup);

            allTitlePopup[0].SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Game")
        {
            DisableAllPopups(allGamePopup);

            allGamePopup[0].SetActive(true);
        }
    }

    public void StartButton()
    {
        SceneLoadManager.Instance.Load(Scene.Game);
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
        DisableAllPopups(allGamePopup);

        allGamePopup[1].SetActive(true);
    }

    public void OutChapterButton()
    {
        DisableAllPopups(allGamePopup);

        allGamePopup[0].SetActive(true);
    }

    void DisableAllPopups(List<GameObject> allPopup)
    {
        for (int i = 0; i < allPopup.Count; i++)
        {
            allPopup[i].SetActive(false);
        }
    }
}
