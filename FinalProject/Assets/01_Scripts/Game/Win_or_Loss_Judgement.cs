using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackType
{
    enum Range
    {
        single,
        multi
    }

    enum Type
    {
        basic,
        heal,
        skill,

    }
}

public class Win_or_Loss_Judgment : MonoBehaviour 
{
    //�θ�
    [SerializeField]
    Transform myTeam;
    [SerializeField]
    Transform enemyTeam;

    List<int> finalSequence = new List<int>();

    //�Է°�
    List<int>  myInputValue = new List<int>();
    List<int> enemyInputValue = new List<int>();

    List<int> myATKspeed = new List<int>();
    List<int> enemyATKspeed = new List<int>();

    //��ü �� ī��Ʈ
    [SerializeField]
    int myTeamCount;
    [SerializeField]
    int enemyTeamCount;

    int player1ATK = 10; 
    int player2ATK = 10; 

    int player1Defense = 5;
    int player2Defense = 5; 

    void Start()
    {
        myTeamCount = myTeam.childCount;
        enemyTeamCount = enemyTeam.childCount;
    }

    public void GetInputSeqeunce(List<int> myInputSkillNum)
    {
        myInputValue = myInputSkillNum;
    }

    //��������, 2��� ���
    public void FightStart()
    {
        SkillCheck();
        SkillSeqencePlay();
    }
 
    void FightEnd()
    {
        Debug.Log("FightEnd");

        if (myTeamCount + enemyTeamCount < 1)
        {
            //BattleEnd();
        }
        else
        {
            //�ڵ����� ��� �Ǵ� �ٽ� 
        }
    }


    public void SkillCheck()
    {       
        //���� ��
        for (int i = 0; i < myTeamCount; i++)
        {
            if (myInputValue[i] != enemyInputValue[i])
            {
                AddSequenceBySkillNumber(i);
            }
            else
            {
                AddSequenceByHalfChance(i);
            }
        }
    
        myATKspeed.Sort(); // �ӵ� ���� ����
        enemyATKspeed.Sort();
    }

    void AddSequenceBySkillNumber(int order)
    {
        if (myInputValue[order] > enemyInputValue[order])
        {
            finalSequence.Add(myInputValue[order]);
        }
        else
        {
            finalSequence.Add(enemyInputValue[order]);
        }
    }

    void AddSequenceByHalfChance(int order)
    {
        // ������ ��� 50% ó��
        bool halfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);
        if (halfATK)
        {
            finalSequence.Add(myInputValue[order]);
        }
        else
        {
            finalSequence.Add(enemyInputValue[order]);
        }
    }

    //����Ʈ ������� ���� (���Ӱ��� ���)
    public void SkillSeqencePlay() 
    {      
        
 

        for (int i = 0; i < finalSequence.Count; i++)
        {
            bool isFasterThanEnemy = myATKspeed[i] > enemyATKspeed[i];

            if (isFasterThanEnemy)
            {
                int num1 = 1;
                if(myATKspeed[i] < enemyATKspeed[i] * 2)
                {
                    Debug.Log(i + "��° ������ ����");
                    i++;
                    continue;
                }

                for (int j = 0; j < Mathf.Abs(enemyATKspeed[i] - (myATKspeed[i] * num1)); j++)
                {
                    Debug.Log(i + "��° �Ʊ��� ����");
                    num1++;
                }

            }
            else if (isFasterThanEnemy && myATKspeed[i] < enemyATKspeed[i] * 2)
            {
                Debug.Log(i + "��° ������ ����");
            }
            else
            {
                if (enemyATKspeed[i] > myATKspeed[i] * 2)
                {
                    float num2 = 1.5f;

                    for (int j = 0; j < Mathf.Abs(myATKspeed[i] - (enemyATKspeed[i] * num2)); j++) 
                    { 
                        Debug.Log(i + "��° ������ ����");
                    }
                }
                else
                {
                    Debug.Log(i + "��° �Ʊ��� ����");
                }
            }

            i++;
        }

        


        FightEnd();
    }

    //���� �� ����, ���� ����ϱ�
    int DamageCalculate()
    {
        int result;
        result = player1ATK - player2Defense;

        return result;
    }
}



