using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyButton : MonoBehaviour
{
    LobbyMainMenu kindOfMenu;
    Button button;
    
    void Start()
    {
        var splitResult = name.Split("_");
        int.TryParse(splitResult[0], out int order);
        kindOfMenu = (LobbyMainMenu)order;
        button = GetComponent<Button>();
        
        button.onClick.AddListener(OnPressButton);
    }

    public void OnPressButton()
    {
        LobbyUIManager.Instance.ShowLobbyMenu(kindOfMenu);
    }
}
