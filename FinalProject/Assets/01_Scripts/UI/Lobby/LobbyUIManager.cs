using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    }

    public void ShowSelectMenu(Vector3 pos, List<string> texts, List<UnityAction> funcList)
    {
        for (int i = 0; i < selectButtonTexts.Length; i++)
        {
            selectButtonTexts[i].text = texts[i];
            selectButtons[i].onClick.AddListener(funcList[i]);
        }
        selectMenu.SetActive(true);
        selectMenuRectTrans.position = pos;
    }

    public void HideSelectMenu()
    {
        selectMenu.SetActive(false);
    }
}
