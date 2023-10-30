using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleJudgment : MonoBehaviour 
{
    //�θ�
    [SerializeField]
    Transform myTeam;
    [SerializeField]
    Transform enemyTeam;

    List<int> finalSequence = new List<int>();

    //�Է°�
    [SerializeField]
    List<int>  myInputValue = new List<int>();

    [SerializeField]
    List<int> enemyInputValue = new List<int>();

    [SerializeField]
    List<int> myATKspeed = new List<int>();

    [SerializeField]
    List<int> enemyATKspeed = new List<int>();

    //��ü �� ī��Ʈ
    [SerializeField]
    int myTeamCount;
    [SerializeField]
    int enemyTeamCount;

    void Start() //���� ���� �� ������Ʈ �ڽĿ� �����ϱ� �Ǵ� �̵� 
    {
        myTeamCount = myTeam.childCount;
        enemyTeamCount = enemyTeam.childCount;
    }

    public void GetInputSeqeunce(List<int> myInputSkillNum)
    {
        myInputValue = myInputSkillNum;
    } //������� �޾ƿ�

    public void GetATKSpeed(List<int> myPlayerSpeed) //������� �޾ƿ�
    {
        myATKspeed = myPlayerSpeed;
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
    
        //myATKspeed.Sort(); // �ӵ� ���� ����
        //enemyATKspeed.Sort();
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
                print("isFasterThanEnemy : true");
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
                print("isFasterThanEnemy : else if");
                Debug.Log(i + "��° ������ ����");
            }
            else
            {
                print("isFasterThanEnemy : false");
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

}



