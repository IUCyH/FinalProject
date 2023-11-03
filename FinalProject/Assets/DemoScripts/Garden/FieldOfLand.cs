using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FieldOfLand : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI testStateDisplay;
    [SerializeField]
    TextMeshProUGUI testSolarDisplay;

    [SerializeField]
    FlowerPerson person;
    string[] selectButtonNames = new[] { "Manage Flower Person", "Get Solar Coin" };
    string[] flowerPersonSelectMenuNames = new[] { "Level Up", "Training", "Upgrade", "Free Action" };
    UnityAction[] selectButtonActions;
    UnityAction[] flowerPersonSelectMenuActions;

    [SerializeField]
    KindOfFlower kindOfFlower;
    [SerializeField]
    bool isFull;
    bool canCollect;
    int solar;
    const int MaxSolar = 10;
    int collectableAmount = 5;
    float maxTime = 1f;
    float timer;

    void Start()
    {
        #region InitMenuArrays
        flowerPersonSelectMenuActions = new UnityAction[]
        {
            () =>
            {
                testStateDisplay.text = "Level Up";
                LobbyUIManager.Instance.HideSelectMenu();
            },
            () =>
            {
                testStateDisplay.text = "Training";
                LobbyUIManager.Instance.HideSelectMenu();
            },
            () =>
            {
                testStateDisplay.text = "Upgrade";
                LobbyUIManager.Instance.HideSelectMenu();
            },
            () =>
            {
                FlowerPersonFreeAction();
                LobbyUIManager.Instance.HideSelectMenu();
            }
        };

        selectButtonActions = new UnityAction[]
        {
            () =>
            {
                var pos = Input.mousePosition;
                LobbyUIManager.Instance.SetSelectMenuPosition(pos);
                LobbyUIManager.Instance.ShowSelectMenu(flowerPersonSelectMenuNames, flowerPersonSelectMenuActions);
            },
            () =>
            {
                if (canCollect)
                {
                    LobbyUIManager.Instance.UpdateSolarCoin(solar);
                    solar = 0;
                    canCollect = false;
                    testSolarDisplay.text = "Solar : 0";
                }

                LobbyUIManager.Instance.HideSelectMenu();
            }
        };
        #endregion

        person = GardenManger.Instance.GetPerson(kindOfFlower);
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject() || ReferenceEquals(person, null)) return;

        LobbyUIManager.Instance.SetSelectMenuPosition(Input.mousePosition);
        LobbyUIManager.Instance.ShowSelectMenu(selectButtonNames, selectButtonActions, 2);
    }

    void Update()
    {
        if (ReferenceEquals(person, null)) return;

        if (!isFull)
        {
            SetPlayerInLand();
        }
        else
        {
            CollectSolar();
        }
    }

    void FlowerPersonFreeAction()
    {
        if (ReferenceEquals(person, null)) return;
        
        person.ActFree();
        person = null;
        isFull = false;
    }

    void SetPlayerInLand()
    {
        person.MoveToField(transform.position);
        
        if (!person.CanAttach)
        {
            isFull = true;
        }
    }

    void CollectSolar()
    {
        if (solar >= MaxSolar)
        {
            testStateDisplay.text = "Sleeping...";
            return;
        }

        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            timer = 0f;
            if (++solar >= collectableAmount)
            {
                canCollect = true;
            }
            testSolarDisplay.text = string.Format("Solar : {0}", solar.ToString()); //for test
        }
    }
}
