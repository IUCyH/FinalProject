using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> allPopUP;

    public void StartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadButton()
    {
        
    }  

    public void SettingButton()
    {
        PopupActiveFalse();

        allPopUP[1].SetActive(true);
    }   

    public void GameOutButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void KeySettingButton()
    {
        PopupActiveFalse();

        allPopUP[2].SetActive(true);
    }

    void PopupActiveFalse()
    {
        for (int i = 0; i < allPopUP.Count; i++)
        {
            allPopUP[i].SetActive(false);
        }
    }
}
