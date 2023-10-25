using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    RectTransform[] btnRectTransforms;
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();
    
    List<int> sequenceValue = new List<int>(); //���õ� ��ư ����

    List<int> sequenceInput = new List<int>(); //1, 2, 3, 4 //��ư ���� ����

    int lastSequence;

    // ��ư�� ������ �� ȣ��� �Լ�
    public void Clickbtn(int btnNum)
    {
        if (sequenceValue.Contains(btnNum)) //&& sequenceValue != null
        {
            var index = sequenceValue.IndexOf(btnNum);
            for (int i = index; i < sequenceValue.Count; i++)
            {
                var buttonNumber = sequenceValue[i];
                UpdateSequenceImages(btnRectTransforms[buttonNumber], i, false);
            }
            sequenceValue.Remove(btnNum);
            
            for (int i = index; i < sequenceValue.Count; i++)
            {
                var buttonNumber = sequenceValue[i];
                UpdateSequenceImages(btnRectTransforms[buttonNumber], i, true);
            }
        }
        else //&& sequenceInput != null 
        {
            if (sequenceValue.Count < 4)
            {
                sequenceValue.Add(btnNum);
                UpdateSequenceImages(btnRectTransforms[btnNum], sequenceValue.Count - 1, true);
            }
        }

    }

    void UpdateSequenceImages(RectTransform rectTransform, int imageNumber, bool isOk)
    {
        sequenceImages[imageNumber].GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
        sequenceImages[imageNumber].SetActive(isOk);
    }
}
