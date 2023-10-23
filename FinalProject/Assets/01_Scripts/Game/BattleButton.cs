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


    //������ ���ʴ�� �ο� ++;
    //1 -> 2 -> 3 ���� ���¿��� 1�� ���ٸ� 1 -> 4


    //���� ���� ���� ū��?
    bool isBigger;

    public void SaveNum(int skillID)
    {
        //if skillID�� �� ������ ��
        isSkill[skillID] = true; //�� ���� ��ư ����

        
        for (int i = 0;  skillNum.Count < i; i++)
        {
            if (skillNum[i] < skillID)
            {
                isBigger = true;
            }
            else
            {
                isBigger = false;
                skillNum[i] += skillAddNum; //0��°���� �ƴϸ� 0 ��ȣ ����
            }
        }

        if (isBigger)
        {
            skillNum[skillID] += skillAddNum; //��ȣ ���� (��ȣ �̹���)
            skillAddNum++;
        }

    }

    public void MinusNum(int skillID)
    {
        //else
        isSkill[skillID] = false; //���� ��ư ����

    }

}
