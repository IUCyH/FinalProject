using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressButton : MonoBehaviour
{
    [SerializeField]
    GameObject settingPopUP;

    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadButton()
    {
        
    }  

    public void SettingButton()
    {
        settingPopUP.SetActive(true);
    }

    public void GameOutButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
