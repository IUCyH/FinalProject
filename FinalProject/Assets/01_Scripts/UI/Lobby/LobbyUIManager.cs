using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    [SerializeField]
    TextMeshProUGUI currentSolarCoinText;
    [SerializeField]
    TextMeshProUGUI currentSunCoinText;
    Dictionary<string, Sprite> uiSprites = new Dictionary<string, Sprite>();

    protected override void OnStart()
    {
        currentSolarCoinText.text = DataManager.Instance.PlayerData.solarCoin.ToString();
        currentSunCoinText.text = DataManager.Instance.PlayerData.sunCoin.ToString();
        exitButtonImg.SetActive(false);

        mainLobbyMenus = mainLobbyMenuParent.GetComponentsInChildren<IWindow>(true);
        selectButtons = selectMenu.GetComponentsInChildren<Button>(true);
        selectButtonTexts = selectMenu.GetComponentsInChildren<TextMeshProUGUI>(true);
        selectMenuRectTrans = selectMenu.GetComponent<RectTransform>();

        var mainMenus = mainLobbyButtonParent.GetComponentsInChildren<MainLobbyButton>(true);
        GetUISprites(mainMenus);
        HideSelectMenu();
        CloseAllLobbyMenu();
    }

    public void UpdateSolarCoin(int amount)
    {
        DataManager.Instance.PlayerData.solarCoin += amount;
        currentSolarCoinText.text = DataManager.Instance.PlayerData.solarCoin.ToString();
        
        DataManager.Instance.Save();
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
        selectMenuRectTrans.position = pos - (new Vector3(selectMenuRectTrans.rect.width, selectMenuRectTrans.rect.height) * 0.5f);
    }

    public void ShowSelectMenu(string[] buttonTexts, UnityAction[] onClickActions, int menuCount = 4)
    {
        if (menuCount > selectButtons.Length) return;
        
        HideSelectMenu();
        for (int i = 0; i < menuCount; i++)
        {
            selectButtons[i].gameObject.SetActive(true);
            selectButtons[i].onClick.AddListener(onClickActions[i]);
            selectButtonTexts[i].text = buttonTexts[i];
        }
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
