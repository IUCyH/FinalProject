using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Demo
{
    public class SelectPopup : Singleton<SelectPopup>
    {
        RectTransform rectTransform;
        [SerializeField]
        Button[] selectButtons;
        [SerializeField]
        TextMeshProUGUI[] selectButtonTexts;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            selectButtons = GetComponentsInChildren<Button>();
            selectButtonTexts = GetComponentsInChildren<TextMeshProUGUI>();
            
            HideSelectButtons();
        }

        public void ShowSelectButtons(Vector3 pos, string[] buttonNames, UnityAction[] actions, int numberOfButtons = 4)
        {
            rectTransform.position = pos;
            
            if (buttonNames.Length > 4)
            {
                Debug.LogError("배열의 크기가 너무 큽니다. 4 이하의 크기로 설정해주세요.");
                return;
            }

            HideSelectButtons();
            for (int i = 0; i < numberOfButtons; i++)
            {
                selectButtons[i].onClick.RemoveAllListeners();
                
                selectButtonTexts[i].text = buttonNames[i];
                selectButtons[i].gameObject.SetActive(true);
                selectButtons[i].onClick.AddListener(actions[i]);
            }
        }

        public void HideSelectButtons()
        {
            for (int i = 0; i < selectButtons.Length; i++)
            {
                selectButtons[i].gameObject.SetActive(false);
            }
        }
    }
}