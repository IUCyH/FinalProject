using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();

    [SerializeField]
    List<int> sequenceOrder = new List<int>(); // ������ �����ϴ� ����Ʈ

    // ��ư�� ������ �� ȣ��� �Լ�
    public void ClickBtn(int btnIndex) //������ȣ ���� �ʿ�
    {
        if (sequenceOrder.Count == 0 || sequenceOrder[sequenceOrder.Count - 1] != btnIndex) //0�̰ų� 
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
