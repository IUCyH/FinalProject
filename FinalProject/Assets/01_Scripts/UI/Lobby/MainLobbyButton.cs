using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyButton : MonoBehaviour
{
    int order;
    GameObject lobbyMenu;
    Button button;
    
    void Start()
    {
        var splitResult = name.Split("_");
        int.TryParse(splitResult[0], out order);
        lobbyMenu = GameObject.Find(name + "Menu");
        button = GetComponent<Button>();
        
        button.onClick.AddListener(OnPressButton);
        
        lobbyMenu.SetActive(false);
    }

    public void OnPressButton()
    {
        LobbyUIManager.Instance.ShowLobbyMenu(order, lobbyMenu);
    }
}
