using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();

    [SerializeField]
    List<int> sequenceOrder = new List<int>(); // 순서를 저장하는 리스트

    // 버튼을 눌렀을 때 호출될 함수
    public void ClickBtn(int btnIndex) //고유번호 지정 필요
    {
        if (sequenceOrder.Count == 0 || sequenceOrder[sequenceOrder.Count - 1] != btnIndex) //0이거나 
        {
            sequenceOrder.Add(btnIndex);
        }
        else
        {
            sequenceOrder.RemoveAt(sequenceOrder.Count - 1);
        }

        UpdateSequenceImages();
    }

    void UpdateSequenceImages()
    {
        for (int i = 0; i < sequenceImages.Count; i++)
        {
            bool isActive = i < sequenceOrder.Count;
            sequenceImages[i].SetActive(isActive);

            if (isActive)
            {
                RectTransform btnTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
                sequenceImages[i].GetComponent<RectTransform>().anchoredPosition = btnTransform.anchoredPosition;
            }
        }
    }
}
