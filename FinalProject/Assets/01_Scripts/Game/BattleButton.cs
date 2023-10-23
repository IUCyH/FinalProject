using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    List<int> skillNum = new List<int>();


    [SerializeField]
    List<bool> isSkill = new List<bool>();

    int skillAddNum;


    //순서를 차례대로 부여 ++;
    //1 -> 2 -> 3 선택 상태에서 1을 뺐다면 1 -> 4


    //들어온 수가 가장 큰가?
    bool isBigger;

    public void SaveNum(int skillID)
    {
        //if skillID가 안 눌렸을 때
        isSkill[skillID] = true; //안 눌린 버튼 설정

        
        for (int i = 0;  skillNum.Count < i; i++)
        {
            if (skillNum[i] < skillID)
            {
                isBigger = true;
            }
            else
            {
                isBigger = false;
                skillNum[i] += skillAddNum; //0번째부터 아니면 0 번호 대입
            }
        }

        if (isBigger)
        {
            skillNum[skillID] += skillAddNum; //번호 설정 (번호 이미지)
            skillAddNum++;
        }

    }

    public void MinusNum(int skillID)
    {
        //else
        isSkill[skillID] = false; //눌린 버튼 해제

    }

}
