using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGC_BattleManager : MonoBehaviour 
{
    #region ValueList
    //�θ�
    [SerializeField]
    Transform p1Team;
    [SerializeField]
    Transform p2Team;

    List<int> finalSequence = new List<int>();

    //�Է°�
    [SerializeField]
    List<int>  p1InputValue = new List<int>();

    [SerializeField]
    List<int> p2InputValue = new List<int>();

    [SerializeField]
    List<int> p1AtkSpeed = new List<int>();

    [SerializeField]
    List<int> p2AtkSpeed = new List<int>();

    //��ü �� ī��Ʈ
    [SerializeField]
    int p1TeamCount;
    [SerializeField]
    int p2TeamCount;
    #endregion

    void Start() //���� ���� �� ������Ʈ �ڽĿ� �����ϱ� �Ǵ� �̵� 
    {
        p1TeamCount = p1Team.childCount;
        p2TeamCount = p2Team.childCount;

    }

    public void GetInputSeqeunce(List<int> myInputSkillNum)
    {
        p1InputValue = myInputSkillNum;
    } //������� �޾ƿ�

    public void GetATKSpeed(List<int> myPlayerSpeed) //������� �޾ƿ�
    {
        p1AtkSpeed = myPlayerSpeed;
    }

    //��������, 2��� ���
    public void FightStart()
    {
        SpeedCheck();
        SkillSeqencePlay();
    }
 
    void FightEnd()
    {
        Debug.Log("FightEnd");

        if (p1TeamCount + p2TeamCount < 1)
        {
            //BattleEnd();
        }
        else
        {
            //�ڵ����� ��� �Ǵ� �ٽ� 
        }
    }


    public void SpeedCheck()
    {       
        //���� ��
        for (int i = 0; i < p1TeamCount; i++)
        {
            if (p1InputValue[i] != p2InputValue[i])
            {
                AddSequenceBySkillNumber(i);
            }
            else
            {
                AddSequenceByHalfChance(i);
            }
        }
    
        //p1AtkSpeed.Sort(); // �ӵ� ���� ����
        //p2AtkSpeed.Sort();
    }

    void AddSequenceBySkillNumber(int order)
    {
        if (p1InputValue[order] > p2InputValue[order])
        {
            finalSequence.Add(p1InputValue[order]);
        }
        else
        {
            finalSequence.Add(p2InputValue[order]);
        }
    }

    void AddSequenceByHalfChance(int order)
    {
        // ������ ��� 50% ó��
        bool halfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);
        if (halfATK)
        {
            finalSequence.Add(p1InputValue[order]);
        }
        else
        {
            finalSequence.Add(p2InputValue[order]);
        }
    }

    //����Ʈ ������� ���� (���Ӱ��� ���)
    public void SkillSeqencePlay() 
    {      

        for (int i = 0; i < finalSequence.Count; i++)
        {
            bool isFasterThanEnemy = p1AtkSpeed[i] > p2AtkSpeed[i];

            if (isFasterThanEnemy)
            {
                print("isFasterThanEnemy : true");
                int num1 = 1;
                if(p1AtkSpeed[i] < p2AtkSpeed[i] * 2)
                {
                    Debug.Log(i + "��° ������ ����");
                    i++;
                    continue;
                }

                for (int j = 0; j < Mathf.Abs(p2AtkSpeed[i] - (p1AtkSpeed[i] * num1)); j++)
                {
                    Debug.Log(i + "��° �Ʊ��� ����");
                    num1++;
                }

            }
            else if (isFasterThanEnemy && p1AtkSpeed[i] < p2AtkSpeed[i] * 2)
            {
                print("isFasterThanEnemy : else if");
                Debug.Log(i + "��° ������ ����");
            }
            else
            {
                print("isFasterThanEnemy : false");
                if (p2AtkSpeed[i] > p1AtkSpeed[i] * 2)
                {
                    float num2 = 1.5f;

                    for (int j = 0; j < Mathf.Abs(p1AtkSpeed[i] - (p2AtkSpeed[i] * num2)); j++) 
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



