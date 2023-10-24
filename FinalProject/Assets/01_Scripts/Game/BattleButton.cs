using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();

    [SerializeField]
    List<int> sequenceValue = new List<int>();

    [SerializeField]
    List<int> sequenceInput = new List<int>(); //1, 2, 3, 4

    // ��ư�� ������ �� ȣ��� �Լ�
    public void Clickbtn(int btnNum) 
    {
        sequenceInput.Sort(); //���� 

        
        int minValue = sequenceInput.Min(); //���� ���� ����  

        
        if (sequenceValue.Contains(btnNum) && sequenceValue != null) //&& sequenceValue != null
        {
            sequenceInput.Add(btnNum);
            sequenceValue.Remove(btnNum);
            UpdateSequenceImages(minValue, false);

        }
        else if (!sequenceValue.Contains(btnNum) && sequenceInput != null) //&& sequenceInput != null 
        {
            if (btnNum == 0)
            {
                btnNum = minValue;
                sequenceInput.Remove(btnNum);
                sequenceValue.Add(btnNum);
                UpdateSequenceImages(minValue, true);
                Debug.Log(btnNum);
            }
        }

    }

    void UpdateSequenceImages(int imageNumber, bool isOk)
    {        
        RectTransform btnTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
        sequenceImages[imageNumber - 1].GetComponent<RectTransform>().anchoredPosition = btnTransform.anchoredPosition;
        sequenceImages[imageNumber - 1].SetActive(isOk);
    }
}
