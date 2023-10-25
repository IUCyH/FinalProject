using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyButton : MonoBehaviour
{
    LobbyMainMenu kindOfMenu;
    
    void Start()
    {
        var splitResult = name.Split("_");
        int.TryParse(splitResult[0], out int order);
        kindOfMenu = (LobbyMainMenu)order;
        var button = GetComponent<Button>();
        var image = GetComponent<Image>();
        
        button.onClick.AddListener(OnPressButton);
        Debug.Log(image.name);
        LobbyUIManager.Instance.SetImageSprite(image, name);
    }

    public void OnPressButton()
    {
        LobbyUIManager.Instance.ShowLobbyMenu(kindOfMenu);
    }
}
