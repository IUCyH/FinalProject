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

    //��ü �� ī��Ʈ
    [SerializeField]
    int myTeamCount; 
    [SerializeField]
    int enemyTeamCount; 


    List<(int ID, int SkillNum)> sequence = new List<(int, int)>();

    List<(int player1ID, int player1SkillNum)> player1Property = new List<(int, int)>(); // ���÷� ���� ������ �߰�
    List<(int player2ID, int player2SkillNum)> player2Property = new List<(int, int)>(); // ���÷� ���� ������ �߰�

    List<(int myInputID, int myInputSkillNum)> myInputValue = new List<(int, int)>();
    List<(int enemyInputID, int enemyInputSkillNum)> enemyInputValue = new List<(int, int)>();

    List<float> myATKspeed = new List <float>();
    List<float> enemyATKspeed = new List<float>();

    float player1health = 100; // ���÷� �ʱⰪ ����
    float player2health = 100; // ���÷� �ʱⰪ ����

    float player1ATK = 10; // ���÷� �ʱⰪ ����
    float player2ATK = 10; // ���÷� �ʱⰪ ����

    float player1Defense = 5; // ���÷� �ʱⰪ ����
    float player2Defense = 5; // ���÷� �ʱⰪ ����

    List<int> player1ATKSpeed = new List<int>(); // ���÷� ���� ������ �߰�
    List<int> player2ATKSpeed = new List<int>(); // ���÷� ���� ������ �߰�

    int allTeamCount;

    void Start()
    {
        for (int m = 0; m < myTeam.childCount; m++)
        {
            myTeamCount++;
        }

        for(int e = 0; e < enemyTeam.childCount; e++)
        {
            enemyTeamCount++;
        }     


        // ���÷� �ʱ� ������ �߰�
        player1Property.Add((10, 1));
        player1Property.Add((20, 2));
        player1Property.Add((30, 3));
        player1Property.Add((40, 4));

        player2Property.Add((50, 5));
        player2Property.Add((60, 6));
        player2Property.Add((70, 7));
        player2Property.Add((80, 8));

        player1ATKSpeed.Add(10);
        player1ATKSpeed.Add(10);
        player1ATKSpeed.Add(10);
        player1ATKSpeed.Add(10);

        player2ATKSpeed.Add(40);
        player2ATKSpeed.Add(40);
        player2ATKSpeed.Add(40);
        player2ATKSpeed.Add(40);

    }

    void Update()
    {
        allTeamCount = myTeamCount + enemyTeamCount; //���� ��
    }

    //��������, 2��� ���
    public void FightStart()
    {   
        SkillSequence();
        SkillCheck();
        SkillSeqencePlay();
    }

    //�ڵ����� ���
    void FightEnd()
    {
        Debug.Log("FightEnd");
    }
    
    public void SkillSequence()
    {

        for (int j = 0; j < myTeamCount; j++)
        {
            myInputValue.Add((1, 2)); // ���÷� �ʱ� ������ �߰�
        }


        for (int k = 0; k < myTeamCount; k++)
        {
            myATKspeed.Add((player1ATKSpeed[k])); // ���÷� �ʱ� ������ �߰�
            
        }
        for (int u = 0; u < myTeamCount; u++)
        {
            enemyATKspeed.Add((player1ATKSpeed[u])); // ���÷� �ʱ� ������ �߰�

        }
        
    }

    public void SkillCheck()
    {
        //���� ��
        for (int i = 0; i < myTeamCount; i++)
        {
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
                // ������ ��� 50% ó��
                bool halfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);
                if (halfATK)
                {
                    sequence.Add(player1Property[i]);
                }
                else
                {
                    sequence.Add(player2Property[i]);
                }
            }
        }

        myATKspeed.Sort(); // �ӵ� ���� ����
        enemyATKspeed.Sort();
    }

    //����, ���� ����ϱ�
    public void SkillSeqencePlay() 
    {
        int i = 0;
        while (i < sequence.Count)
        {
            if (player1ATKSpeed[i] > player2ATKSpeed[i])
            {
                if (player1ATKSpeed[i] > player2ATKSpeed[i] * 2)
                {
                    int num1 = 1;
                    while (player2ATKSpeed[i] - (20 * num1) < player1ATKSpeed[i])
                    {
                        Debug.Log(i + "��° �Ʊ��� ����");
                        num1++;
                    }
                }
                else
                {
                    Debug.Log(i + "��° ������ ����");
                }
            }
            else
            {
                if (player2ATKSpeed[i] > player1ATKSpeed[i] * 2)
                {
                    int num2 = 1;
                    while (player1ATKSpeed[i] - (20 * num2) < player2ATKSpeed[i])
                    {
                        Debug.Log(i + "��° ������ ����");
                        num2++;
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
}


