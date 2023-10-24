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

    List<(int ID, int SkillNum)> sequence = new List<(int, int)>();

    List<(int player1ID, int player1SkillNum)> player1Property = new List<(int, int)>(); // 예시로 더미 데이터 추가
    List<(int player2ID, int player2SkillNum)> player2Property = new List<(int, int)>(); // 예시로 더미 데이터 추가

    List<(int myInputID, int myInputSkillNum)> myInputValue = new List<(int, int)>();
    List<(int enemyInputID, int enemyInputSkillNum)> enemyInputValue = new List<(int, int)>();

    List<float> myATKspeed = new List<float>();
    List<float> enemyATKspeed = new List<float>();

    List<int> player1ATKSpeed = new List<int>(); // 예시로 더미 데이터 추가
    List<int> player2ATKSpeed = new List<int>(); // 예시로 더미 데이터 추가

    float player1health = 100; // 예시로 초기값 설정
    float player2health = 100; // 예시로 초기값 설정

    //개체 수 카운트
    [SerializeField]
    int myTeamCount;
    [SerializeField]
    int enemyTeamCount;

    int player1ATK = 10; // 예시로 초기값 설정
    int player2ATK = 10; // 예시로 초기값 설정

    int player1Defense = 5; // 예시로 초기값 설정
    int player2Defense = 5; // 예시로 초기값 설정

    

    int allTeamCount;

    void Start()
    {
        
        for (int m = 0; m < myTeam.childCount; m++)
        {
            myTeamCount++;
        }

        for (int e = 0; e < enemyTeam.childCount; e++)
        {
            enemyTeamCount++;
        }


        // 예시로 초기 데이터 추가
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
        allTeamCount = myTeamCount + enemyTeamCount; //남은 수
    }

    //전투실행, 2배속 고려
    public void FightStart()
    {
        SkillSequence();
        SkillCheck();
        SkillSeqencePlay();
    }

    //자동전투 고려
    void FightEnd()
    {
        Debug.Log("FightEnd");
    }

    public void SkillSequence()
    {
        
        for (int j = 0; j < myTeamCount; j++)
        {
            myInputValue.Add((1, 2)); // 예시로 초기 데이터 추가
        }


        for (int k = 0; k < myTeamCount; k++)
        {
            myATKspeed.Add(player1ATKSpeed[k]); // 예시로 초기 데이터 추가

        }
        for (int u = 0; u < myTeamCount; u++)
        {
            enemyATKspeed.Add(player1ATKSpeed[u]); // 예시로 초기 데이터 추가

        }

    }

    public void SkillCheck()
    {       
        //공속 비교
        for (int i = 0; i < myTeamCount; i++)
        {
            if (player1Property[i].player1SkillNum != player2Property[i].player2SkillNum)
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
        if (player1Property[order].player1SkillNum > player2Property[order].player2SkillNum)
        {
            sequence.Add(player1Property[order]);
        }
        else
        {
            sequence.Add(player2Property[order]);
        }
    }

    void AddSequenceByHalfChance(int order)
    {
        // 동일한 경우 50% 처리
        bool halfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);
        if (halfATK)
        {
            sequence.Add(player1Property[order]);
        }
        else
        {
            sequence.Add(player2Property[order]);
        }
    }

    //공식, 수식 계산하기
    int DamageCalculate()
    {
        int result;
        result = player1ATK - player2Defense;

        return result;
    }

    
    public void SkillSeqencePlay() 
    {      
        
        int i = 0;
        while (i < sequence.Count)
        {
            bool isFasterThanPlayer2 = player1ATKSpeed[i] > player2ATKSpeed[i];

            if (isFasterThanPlayer2)
            {
                int num1 = 1;
                if(player1ATKSpeed[i] < player2ATKSpeed[i] * 2)
                {
                    Debug.Log(i + "번째 적군이 떄림");
                    i++;
                    continue;
                }

                for (int j = 0; j < Mathf.Abs(player2ATKSpeed[i] - (player1ATKSpeed[i] * num1)); j++)
                {
                    Debug.Log(i + "번째 아군이 떄림");
                    num1++;
                }

            }
            else if (isFasterThanPlayer2 && player1ATKSpeed[i] < player2ATKSpeed[i] * 2)
            {
                Debug.Log(i + "번째 적군이 떄림");
            }
            else
            {
                if (player2ATKSpeed[i] > player1ATKSpeed[i] * 2)
                {
                    float num2 = 1.5f;

                    for (int j = 0; j < Mathf.Abs(player1ATKSpeed[i] - (player2ATKSpeed[i] * num2)); j++) 
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
}



