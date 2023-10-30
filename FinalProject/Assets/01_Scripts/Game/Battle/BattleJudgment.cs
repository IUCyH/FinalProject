using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleJudgment : MonoBehaviour 
{
    //부모
    [SerializeField]
    Transform myTeam;
    [SerializeField]
    Transform enemyTeam;

    List<int> finalSequence = new List<int>();

    //입력값
    [SerializeField]
    List<int>  myInputValue = new List<int>();

    [SerializeField]
    List<int> enemyInputValue = new List<int>();

    [SerializeField]
    List<int> myATKspeed = new List<int>();

    [SerializeField]
    List<int> enemyATKspeed = new List<int>();

    //개체 수 카운트
    [SerializeField]
    int myTeamCount;
    [SerializeField]
    int enemyTeamCount;

    void Start() //전투 시작 시 오브젝트 자식에 생성하기 또는 이동 
    {
        myTeamCount = myTeam.childCount;
        enemyTeamCount = enemyTeam.childCount;
    }

    public void GetInputSeqeunce(List<int> myInputSkillNum)
    {
        myInputValue = myInputSkillNum;
    } //순서대로 받아옴

    public void GetATKSpeed(List<int> myPlayerSpeed) //순서대로 받아옴
    {
        myATKspeed = myPlayerSpeed;
    }

    //전투실행, 2배속 고려
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
            //자동전투 고려 또는 다시 
        }
    }


    public void SkillCheck()
    {       
        //공속 비교
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
    
        //myATKspeed.Sort(); // 속도 정보 정렬
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
        // 동일한 경우 50% 처리
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

    //리스트 순서대로 실행 (연속공격 고려)
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
                    Debug.Log(i + "번째 적군이 떄림");
                    i++;
                    continue;
                }

                for (int j = 0; j < Mathf.Abs(enemyATKspeed[i] - (myATKspeed[i] * num1)); j++)
                {
                    Debug.Log(i + "번째 아군이 떄림");
                    num1++;
                }

            }
            else if (isFasterThanEnemy && myATKspeed[i] < enemyATKspeed[i] * 2)
            {
                print("isFasterThanEnemy : else if");
                Debug.Log(i + "번째 적군이 떄림");
            }
            else
            {
                print("isFasterThanEnemy : false");
                if (enemyATKspeed[i] > myATKspeed[i] * 2)
                {
                    float num2 = 1.5f;

                    for (int j = 0; j < Mathf.Abs(myATKspeed[i] - (enemyATKspeed[i] * num2)); j++) 
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



