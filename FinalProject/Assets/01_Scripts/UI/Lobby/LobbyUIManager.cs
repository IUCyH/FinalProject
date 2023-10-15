using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum OrderOfButton
{
    First,
    Second,
    Third,
    Fourth
}

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    [SerializeField]
    GameObject exitButtonImg;
    [SerializeField]
    GameObject selectMenu;
    [SerializeField]
    GameObject mainLobbyUI;
    [SerializeField]
    GameObject mainLobbyMenuBackgrounds;
    RectTransform selectMenuRectTrans;
    Image[] lobbyMenuBackgrounds;
    Button[] mainLobbyButtons;
    Button[] selectButtons;
    TextMeshProUGUI[] selectButtonTexts;

    protected override void OnStart()
    {
        exitButtonImg.SetActive(false);
        
        mainLobbyButtons = mainLobbyUI.GetComponentsInChildren<Button>();
        lobbyMenuBackgrounds = mainLobbyMenuBackgrounds.GetComponentsInChildren<Image>(true);
            
        selectButtons = selectMenu.GetComponentsInChildren<Button>();
        selectButtonTexts = selectMenu.GetComponentsInChildren<TextMeshProUGUI>();
        selectMenuRectTrans = selectMenu.GetComponent<RectTransform>();
        
        HideSelectMenu();
        HideAllLobbyMenuBackgrounds();
    }

    public void ShowLobbyMenu(int order, GameObject menu)
    {
        mainLobbyUI.SetActive(false);
        lobbyMenuBackgrounds[order].enabled = true;
        WindowManager.Instance.OpenWindowAndPush(menu);

        exitButtonImg.SetActive(true);
    }

    public void OnPressExitMenuButton()
    {
        HideAllLobbyMenuBackgrounds();
        WindowManager.Instance.CloseWindowAndPop();
        mainLobbyUI.SetActive(true);

        exitButtonImg.SetActive(false);
    }

    public void SetSelectMenuPosition(Vector3 pos)
    {
        selectMenuRectTrans.position = pos;
    }

    public void ShowSelectButton(OrderOfButton order, string buttonText, UnityAction onClickAction)
    {
        var button = selectButtons[(int)order];
        
        button.gameObject.SetActive(true);
        button.onClick.AddListener(onClickAction);
        selectButtonTexts[(int)order].text = buttonText;
    }

    public void HideSelectMenu()
    {
        for (int i = 0; i < selectButtons.Length; i++)
        {
            selectButtons[i].onClick.RemoveAllListeners();
            selectButtons[i].gameObject.SetActive(false);
        }
    }

    void HideAllLobbyMenuBackgrounds()
    {
        for (int i = 0; i < lobbyMenuBackgrounds.Length; i++)
        {
            lobbyMenuBackgrounds[i].enabled = false;
        }
    }
}
