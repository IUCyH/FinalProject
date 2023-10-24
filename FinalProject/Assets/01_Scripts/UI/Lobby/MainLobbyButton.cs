using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyButton : MonoBehaviour
{
    LobbyMainMenu kindOfMenu;
    Button button;
    Image image;
    
    void Start()
    {
        var splitResult = name.Split("_");
        int.TryParse(splitResult[0], out int order);
        kindOfMenu = (LobbyMainMenu)order;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        
        button.onClick.AddListener(OnPressButton);
        //image.sprite = DataManager.Instance.GetSprite()
    }

    public void OnPressButton()
    {
        LobbyUIManager.Instance.ShowLobbyMenu(kindOfMenu);
    }
}
