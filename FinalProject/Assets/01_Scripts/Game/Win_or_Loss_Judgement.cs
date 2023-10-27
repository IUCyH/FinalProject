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
    //부모
    [SerializeField]
    Transform myTeam;
    [SerializeField]
    Transform enemyTeam;

    List<int> finalSequence = new List<int>();

    //입력값
    List<int>  myInputValue = new List<int>();
    List<int> enemyInputValue = new List<int>();

    List<int> myATKspeed = new List<int>();
    List<int> enemyATKspeed = new List<int>();

    //개체 수 카운트
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
    
        myATKspeed.Sort(); // 속도 정보 정렬
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
                Debug.Log(i + "번째 적군이 떄림");
            }
            else
            {
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
    int DamageCalculate()
    {
        int result;
        result = player1ATK - player2Defense;

        return result;
    }
}



