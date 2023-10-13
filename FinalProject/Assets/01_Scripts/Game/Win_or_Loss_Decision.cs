using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Win_or_Loss_Decision : MonoBehaviour
{

    int myTeam;
    int enemyTeam;

    List<(int ID, int SkillNum)> sequence = new List<(int, int)>();

    List<(int player1ID, int player1SkillNum)> player1Property;
    List<(int player2ID, int player2SkillNum)> player2Property;

    List<(int InputID, int InputSkillNum)> InputValue = new List<(int, int)>();

    //���ݷ�, ġ��Ÿ, �� �����, ����� ���, �����, ü�� �����, ����, ȸ��, ���� �ݻ�, �ӵ� 
    float player1health;
    float player2health;

    float player1ATK;
    float player2ATK;

    float player1Defense;
    float player2Defense;

    float player1attackSpeed;
    float player2attackSpeed;


    public void SkillSequence((int choiceID, int choiceSkill) choiceSequence)
    {
        while (myTeam + enemyTeam > 0)
        {
            for (int i = 0; (player1Property.Count + player2Property.Count) < i; i++)
            {
                //���� ���� �ֱ�
                for (int j = 0; j > myTeam; j++)
                {
                    InputValue.Add(choiceSequence);
                }

                if (player1Property[i].player1SkillNum != player2Property[i].player2SkillNum)
                {
                    if (player1Property[i].player1SkillNum > player2Property[i].player2SkillNum)
                    {
                        sequence.Add(player1Property[i]);
                    }
                    else
                    {
                        sequence.Add(player2Property[i]);
                    }
                }
                else
                {
                    if (player1attackSpeed > player2attackSpeed)
                    {
                        sequence.Add(player1Property[i]);
                    }
                    else
                    {
                        sequence.Add(player2Property[i]);
                    }
                }
            }
        }
    }

    void Skillexecution()
    {
        //sequence�� ������� ����
    }
}
