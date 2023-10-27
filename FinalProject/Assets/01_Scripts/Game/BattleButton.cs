using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    RectTransform[] btnRectTransforms;
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();

    [SerializeField]
    List<int> sequenceValue = new List<int>(); //선택된 버튼 저장


    // 버튼을 눌렀을 때 호출될 함수
    //public void Clickbtn(int btnNum)
    //{
    //    if (sequenceValue.Contains(btnNum)) //&& sequenceValue != null
    //    {
    //        var index = sequenceValue.IndexOf(btnNum);
    //        for (int i = index; i < sequenceValue.Count; i++)
    //        {
    //            var buttonNumber = sequenceValue[i];
    //            UpdateSequenceImages(btnRectTransforms[buttonNumber], i, false);
               
    //        }
    //        sequenceValue.Remove(btnNum);

    //        for (int i = index; i < sequenceValue.Count; i++)
    //        {
    //            var buttonNumber = sequenceValue[i];
    //            UpdateSequenceImages(btnRectTransforms[buttonNumber], i, true);

    //        }
    //    }
    //    else //&& sequenceInput != null 
    //    {
    //        if (sequenceValue.Count < 4)
    //        {
    //            sequenceValue.Add(btnNum);          
    //            UpdateSequenceImages(btnRectTransforms[btnNum], sequenceValue.Count - 1, true);

                
    //        }
    //    }
    //}
    
    
    void UpdateSequenceImages(RectTransform rectTransform, int imageNumber, bool isOk)
    {
        sequenceImages[imageNumber].GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
        sequenceImages[imageNumber].SetActive(isOk);
    }

    [SerializeField]
    Win_or_Loss_Judgment judgment;

    [SerializeField]
    List<GameObject> sonButtons = new List<GameObject>(); //자식 버튼들 

    [SerializeField]
    List<bool> isSetButton = new List<bool>(); //8개


    public void ClickBtn()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        for (int i = 0;i < 8; i += 2)
        {
            int first = i;
            int second = i + 1;

            int nothing = 0;
            int plusOne = 1;

            if (sonButtons[first] == clickObject)
            {
                TwiceChoiceButton(i, nothing, plusOne);
            }
            else if (sonButtons[second] == clickObject)
            {
                TwiceChoiceButton(i, plusOne, nothing);          
            }
        }
    }

    void TwiceChoiceButton(int i, int num1, int num2)
    {
        if (isSetButton[i + num1] == false)
        {
            print("falseCheck");
            if (isSetButton[i + num2] == true)
            {

                isSetButton[i + num2] = false;
                sequenceValue.Remove(i + num2);
                UpdateSequenceImages(btnRectTransforms[i + num2], i + num2, false);
            }

            isSetButton[i + num1] = true;
            sequenceValue.Add(i + num1);
            UpdateSequenceImages(btnRectTransforms[i + num1], i + num1, true);

        }
        else
        {
            print("trueCheck");

            isSetButton[i + num1] = false;

            sequenceValue.Remove(i + num1);
            UpdateSequenceImages(btnRectTransforms[i + num1], i + num1, false);

        }
    }



    void SetActiveCheckButtons()
    {
        for (int i = 0; i < 8; i += 2)
        {
            if (isSetButton[i] == false && isSetButton[i + 1] == false)
            {
                RandomChoiceButtons(i);
            }
        }
    }

    void RandomChoiceButtons(int activeButtons)
    {
        bool halfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);
        if (halfATK)
        {
            print("dodo");
            isSetButton[activeButtons] = true;
            sequenceValue.Add(activeButtons);
            UpdateSequenceImages(btnRectTransforms[activeButtons], activeButtons, true);
        }
        else
        {
            print("do");
            isSetButton[activeButtons + 1] = true;
            sequenceValue.Add(activeButtons + 1);
            UpdateSequenceImages(btnRectTransforms[activeButtons + 1], activeButtons + 1, true);
        }
    }

    public void EndOfTurnButton()
    {
        SetActiveCheckButtons();
        judgment.FightStart();
    }
}
