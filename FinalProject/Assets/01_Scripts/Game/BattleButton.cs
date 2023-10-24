using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();

    [SerializeField]
    List<int> sequenceOrder = new List<int>(); // 순서를 저장하는 리스트 (4)

    [SerializeField]
    List<bool> numberList = new List<bool>(); //4

    // 버튼을 눌렀을 때 호출될 함수
    public void ClickBtn() //번호 지정 필요
    {
        for (int i = 0; i < numberList.Count; i++)
        {
            if (numberList[i] == false)
            {
                Debug.Log("false");
                numberList[i] = true;
                sequenceOrder.Add(i);
       

                UpdateSequenceImages(i, numberList[i]);
                break;
            }
            else
            {
                for (int j = 0; j < sequenceOrder.Count; j++)
                {
                    if (sequenceOrder.Contains(sequenceOrder[j]))
                    {
                        Debug.Log("true");
                        numberList[i] = false;
                        sequenceOrder.Remove(i);
             

                        UpdateSequenceImages(i, numberList[i]);

                        break;
                    }
                }
            }
        }
    }


    void UpdateSequenceImages(int imageNumber, bool isOk)
    {        
        RectTransform btnTransform = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
        sequenceImages[imageNumber].GetComponent<RectTransform>().anchoredPosition = btnTransform.anchoredPosition;
        sequenceImages[imageNumber].SetActive(isOk);
    }
}
