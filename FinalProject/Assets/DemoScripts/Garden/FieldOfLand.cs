using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Demo
{
    public class FieldOfLand : MonoBehaviour
    {
        [SerializeField]
        FlowerPerson person;
        string[] selectButtonNames = new[] { "Manage Flower Person", "Get Solar Coin"};
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
            flowerPersonSelectMenuActions = new UnityAction[]
            {
                () =>
                {
                    SelectPopup.Instance.HideSelectButtons();
                    Debug.Log("Level Up");
                },
                () =>
                {
                    SelectPopup.Instance.HideSelectButtons();
                    Debug.Log("Training");
                },
                () =>
                {
                    SelectPopup.Instance.HideSelectButtons();
                    Debug.Log("Upgrade");
                },
                () =>
                {
                    SelectPopup.Instance.HideSelectButtons();
                    FlowerPersonFreeAction();
                }
            };
            
            selectButtonActions = new UnityAction[]
            {
                () =>
                {
                    var pos = Camera.main.WorldToScreenPoint(transform.position);
                    SelectPopup.Instance.ShowSelectButtons(pos, flowerPersonSelectMenuNames, flowerPersonSelectMenuActions);
                },
                () =>
                {
                    SelectPopup.Instance.HideSelectButtons();
                    if (canCollect)
                    {
                        Debug.Log("Take All Solar : " + solar);
                        solar = 0;
                        canCollect = false;
                    }
                }
            };
        }

        void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            Debug.Log(name);
            if (canCollect)
            {
                Debug.Log("Take All Solar : " + solar);

                solar = 0;
                canCollect = false;
            }

            var pos = Camera.main.WorldToScreenPoint(transform.position);
            SelectPopup.Instance.ShowSelectButtons(pos, selectButtonNames, selectButtonActions, 2);
        }

        void Update()
        {
            if (!isFull)
            {
                if (ReferenceEquals(person, null))
                    person = GardenManger.Instance.GetPerson(kindOfFlower);

                if (!ReferenceEquals(person, null))
                {
                    isFull = person.MoveToField(transform.position);
                }
            }
            else
            {
                if (solar >= MaxSolar) return;
                
                timer += Time.deltaTime;

                if (timer > maxTime)
                {
                    timer = 0f;
                    Debug.Log(solar++);
                }

                if (solar >= collectableAmount)
                {
                    canCollect = true;
                }

                if (solar >= MaxSolar)
                {
                    Debug.Log("Sleeping.....");
                }
            }
        }

        void FlowerPersonFreeAction()
        {
            if (!ReferenceEquals(person, null))
            {
                person.ActFree();
                person = null;
                isFull = false;
            }
        }
    }
}
