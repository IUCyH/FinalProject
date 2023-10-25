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
    GameObject mainLobbyButtonParent;
    [SerializeField]
    GameObject mainLobbyMenuParent;
    RectTransform selectMenuRectTrans;
    Button[] selectButtons;
    TextMeshProUGUI[] selectButtonTexts;
    Dictionary<string, Sprite> uiSprites = new Dictionary<string, Sprite>();

    protected override void OnStart()
    {
        exitButtonImg.SetActive(false);

        mainLobbyMenus = mainLobbyMenuParent.GetComponentsInChildren<IWindow>(true);
        selectButtons = selectMenu.GetComponentsInChildren<Button>();
        selectButtonTexts = selectMenu.GetComponentsInChildren<TextMeshProUGUI>();
        selectMenuRectTrans = selectMenu.GetComponent<RectTransform>();

        var mainMenus = mainLobbyButtonParent.GetComponentsInChildren<MainLobbyButton>(true);
        GetUISprites(mainMenus);
        HideSelectMenu();
        CloseAllLobbyMenu();
    }

    public void ShowLobbyMenu(LobbyMainMenu kindOfMenu)
    {
        var menu = mainLobbyMenus[(int)kindOfMenu];
        WindowManager.Instance.OpenWindowAndPush(menu);

        if (kindOfMenu != LobbyMainMenu.OptionsMenu)
        {
            mainLobbyButtonParent.SetActive(false);
            exitButtonImg.SetActive(true);
        }
    }

    public void OnPressExitMenuButton()
    {
        WindowManager.Instance.CloseWindowAndPop();
        mainLobbyButtonParent.SetActive(true);

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

    public void SetImageSprite(Image image, string spriteName)
    {
        if (uiSprites.TryGetValue(spriteName, out Sprite sprite))
        {
            image.sprite = sprite;
        }
    }

    void CloseAllLobbyMenu()
    {
        for (int i = 0; i < mainLobbyMenus.Length; i++)
        {
            mainLobbyMenus[i].Close();
        }
    }

    void GetUISprites(MainLobbyButton[] mainMenus)
    {
        for (int i = 0; i < mainMenus.Length; i++)
        {
            var name = mainMenus[i].name;
            Debug.Log(name);
            var split = name.Split('_');
            var spriteName = string.Format("UI_{0}.png", split[1]);
            var sprite = DataManager.Instance.GetSprite(SpriteAssetBundleTag.UI, spriteName);
            
            uiSprites.Add(name, sprite);
        }
    }
}
