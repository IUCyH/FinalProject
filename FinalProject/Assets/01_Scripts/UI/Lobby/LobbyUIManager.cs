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

public enum LobbyMainMenu
{
    StoreMenu,
    FriendsMenu,
    AchievementsMenu,
    QuestMenu,
    SelectChapterMenu,
    EditGardenMenu,
    MailBoxMenu,
    EventsMenu,
    OptionsMenu
}

public class LobbyUIManager : Singleton<LobbyUIManager>
{
    IWindow[] mainLobbyMenus;
    [SerializeField]
    GameObject exitButtonImg;
    [SerializeField]
    GameObject selectMenu;
    [SerializeField]
    GameObject mainLobbyUI;
    [SerializeField]
    GameObject mainLobbyMenu;
    RectTransform selectMenuRectTrans;
    Image[] lobbyMenuBackgrounds;
    Button[] selectButtons;
    TextMeshProUGUI[] selectButtonTexts;

    protected override void OnStart()
    {
        exitButtonImg.SetActive(false);

        mainLobbyMenus = mainLobbyMenu.GetComponentsInChildren<IWindow>(true);
        selectButtons = selectMenu.GetComponentsInChildren<Button>();
        selectButtonTexts = selectMenu.GetComponentsInChildren<TextMeshProUGUI>();
        selectMenuRectTrans = selectMenu.GetComponent<RectTransform>();
        
        HideSelectMenu();
        CloseAllLobbyMenu();
    }

    public void ShowLobbyMenu(LobbyMainMenu kindOfMenu)
    {
        var menu = mainLobbyMenus[(int)kindOfMenu];
        WindowManager.Instance.OpenWindowAndPush(menu);

        if (kindOfMenu != LobbyMainMenu.OptionsMenu)
        {
            mainLobbyUI.SetActive(false);
            exitButtonImg.SetActive(true);
        }
    }

    public void OnPressExitMenuButton()
    {
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

    void CloseAllLobbyMenu()
    {
        for (int i = 0; i < mainLobbyMenus.Length; i++)
        {
            mainLobbyMenus[i].Close();
        }
    }
}
