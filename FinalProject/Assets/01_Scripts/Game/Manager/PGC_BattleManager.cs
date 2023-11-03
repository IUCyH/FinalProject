using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGC_BattleManager : MonoBehaviour 
{
    #region ValueList
    //부모
    [SerializeField]
    Transform p1Team;
    [SerializeField]
    Transform p2Team;

    List<int> finalSequence = new List<int>();

    //입력값
    [SerializeField]
    List<int>  p1InputValue = new List<int>();

    [SerializeField]
    List<int> p2InputValue = new List<int>();

    [SerializeField]
    List<int> p1AtkSpeed = new List<int>();

    [SerializeField]
    List<int> p2AtkSpeed = new List<int>();

    //개체 수 카운트
    [SerializeField]
    int p1TeamCount;
    [SerializeField]
    int p2TeamCount;
    #endregion

    void Start() //전투 시작 시 오브젝트 자식에 생성하기 또는 이동 
    {
        p1TeamCount = p1Team.childCount;
        p2TeamCount = p2Team.childCount;

    }

    public void GetInputSeqeunce(List<int> myInputSkillNum)
    {
        p1InputValue = myInputSkillNum;
    } //순서대로 받아옴

    public void GetATKSpeed(List<int> myPlayerSpeed) //순서대로 받아옴
    {
        p1AtkSpeed = myPlayerSpeed;
    }

    //전투실행, 2배속 고려
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
            //자동전투 고려 또는 다시 
        }
    }


    public void SpeedCheck()
    {       
        //공속 비교
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
    
        //p1AtkSpeed.Sort(); // 속도 정보 정렬
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
        // 동일한 경우 50% 처리
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

    //리스트 순서대로 실행 (연속공격 고려)
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
                    Debug.Log(i + "번째 적군이 떄림");
                    i++;
                    continue;
                }

                for (int j = 0; j < Mathf.Abs(p2AtkSpeed[i] - (p1AtkSpeed[i] * num1)); j++)
                {
                    Debug.Log(i + "번째 아군이 떄림");
                    num1++;
                }

            }
            else if (isFasterThanEnemy && p1AtkSpeed[i] < p2AtkSpeed[i] * 2)
            {
                print("isFasterThanEnemy : else if");
                Debug.Log(i + "번째 적군이 떄림");
            }
            else
            {
                print("isFasterThanEnemy : false");
                if (p2AtkSpeed[i] > p1AtkSpeed[i] * 2)
                {
                    float num2 = 1.5f;

                    for (int j = 0; j < Mathf.Abs(p1AtkSpeed[i] - (p2AtkSpeed[i] * num2)); j++) 
                    { 
                        Debug.Log(i + "번째 적군이 떄림");
                    }
                }
                else
                {
                    Debug.Log(i + "번째 아군이 떄림");
                }
            }

            i++;
        }

        


        FightEnd();
    }

    //때릴 떄 공식, 수식 계산하기

}



