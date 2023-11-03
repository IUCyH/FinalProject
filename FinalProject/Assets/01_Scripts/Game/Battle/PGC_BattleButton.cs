using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PGC_BattleButton : MonoBehaviour
{
    [SerializeField]
    RectTransform[] btnRectTransforms;
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();

    [SerializeField]
    List<int> sequenceValue = new List<int>(); //선택된 버튼 저장 *비어있음 전투 끝나면 초기화

    [SerializeField]
    List<int> sequencePlayer = new List<int>();//플레이어 순서 *비어있음 전투 끝나면 초기화

    [SerializeField]
    PGC_BattleManager battleManager;

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
            if (isSetButton[i + num2] == true)
            {
                isSetButton[i + num2] = false;
                isSetButton[i + num1] = true;

                for (int j = 0; j < sequenceValue.Count; j++)
                {
                    if (sequenceValue[j] == i + num2)
                    {
                        sequenceValue[j] = i + num1;
                        break;
                    }
                }

                UpdateSequenceImages(btnRectTransforms[i + num2], i + num2, false);
                UpdateSequenceImages(btnRectTransforms[i + num1], i + num1, true);
            }
            else
            {

                isSetButton[i + num1] = true;
                sequenceValue.Add(i + num1);
                UpdateSequenceImages(btnRectTransforms[i + num1], i + num1, true);
            }
        }
        else
        {
            isSetButton[i + num1] = false;
            sequenceValue.Remove(i + num1);
            UpdateSequenceImages(btnRectTransforms[i + num1], i + num1, false);

        }
    }

    void UpdateSequenceImages(RectTransform rectTransform, int imageNumber, bool isOk)
    {
        sequenceImages[imageNumber].GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
        sequenceImages[imageNumber].SetActive(isOk);
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
            isSetButton[activeButtons] = true;
            sequenceValue.Add(activeButtons);
            UpdateSequenceImages(btnRectTransforms[activeButtons], activeButtons, true);
        }
        else
        {
            isSetButton[activeButtons + 1] = true;
            sequenceValue.Add(activeButtons + 1);
            UpdateSequenceImages(btnRectTransforms[activeButtons + 1], activeButtons + 1, true);
        }
    }
  
    void PlayerSort()
    {
        for (int i = 0; i < sequenceValue.Count; i++)
        {
            if (sequenceValue[i] == 0 || sequenceValue[i] == 1)
            {
                sequencePlayer.Add(1);
            }
            else if (sequenceValue[i] == 2 || sequenceValue[i] == 3)
            {
                sequencePlayer.Add(2);
            }
            else if (sequenceValue[i] == 4 || sequenceValue[i] == 5)
            {
                sequencePlayer.Add(3);
            }
            else if (sequenceValue[i] == 6 || sequenceValue[i] == 7)
            {
                sequencePlayer.Add(4);
            }
        }
    }

    public void EndOfTurnButton()
    {
        SetActiveCheckButtons();
        PlayerSort();
        battleManager.GetInputSeqeunce(sequencePlayer);
        battleManager.FightStart();
    }
}
