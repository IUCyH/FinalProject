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
    GameObject selectMenu;
    RectTransform selectMenuRectTrans;
    Button[] selectButtons;
    TextMeshProUGUI[] selectButtonTexts;

    protected override void OnStart()
    {
        selectButtons = selectMenu.GetComponentsInChildren<Button>();
        selectButtonTexts = selectMenu.GetComponentsInChildren<TextMeshProUGUI>();
        selectMenuRectTrans = selectMenu.GetComponent<RectTransform>();
        HideSelectMenu();
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
}
