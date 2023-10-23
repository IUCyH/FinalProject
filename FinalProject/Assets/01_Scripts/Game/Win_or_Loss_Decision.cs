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

    //개체 수 카운트
    [SerializeField]
    int myTeamCount; 
    [SerializeField]
    int enemyTeamCount; 


    List<(int ID, int SkillNum)> sequence = new List<(int, int)>();

    List<(int player1ID, int player1SkillNum)> player1Property = new List<(int, int)>(); // 예시로 더미 데이터 추가
    List<(int player2ID, int player2SkillNum)> player2Property = new List<(int, int)>(); // 예시로 더미 데이터 추가

    List<(int myInputID, int myInputSkillNum)> myInputValue = new List<(int, int)>();
    List<(int enemyInputID, int enemyInputSkillNum)> enemyInputValue = new List<(int, int)>();

    List<float> myATKspeed = new List <float>();
    List<float> enemyATKspeed = new List<float>();

    float player1health = 100; // 예시로 초기값 설정
    float player2health = 100; // 예시로 초기값 설정

    float player1ATK = 10; // 예시로 초기값 설정
    float player2ATK = 10; // 예시로 초기값 설정

    float player1Defense = 5; // 예시로 초기값 설정
    float player2Defense = 5; // 예시로 초기값 설정

    List<int> player1ATKSpeed = new List<int>(); // 예시로 더미 데이터 추가
    List<int> player2ATKSpeed = new List<int>(); // 예시로 더미 데이터 추가

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
            myATKspeed.Add((player1ATKSpeed[k])); // 예시로 초기 데이터 추가
            
        }
        for (int u = 0; u < myTeamCount; u++)
        {
            enemyATKspeed.Add((player1ATKSpeed[u])); // 예시로 초기 데이터 추가

        }
        
    }

    public void SkillCheck()
    {
        //공속 비교
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
                // 동일한 경우 50% 처리
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

        myATKspeed.Sort(); // 속도 정보 정렬
        enemyATKspeed.Sort();
    }

    //공식, 수식 고려하기
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
                        Debug.Log(i + "번째 아군이 떄림");
                        num1++;
                    }
                }
                else
                {
                    Debug.Log(i + "번째 적군이 떄림");
                }
            }
            else
            {
                if (player2ATKSpeed[i] > player1ATKSpeed[i] * 2)
                {
                    int num2 = 1;
                    while (player1ATKSpeed[i] - (20 * num2) < player2ATKSpeed[i])
                    {
                        Debug.Log(i + "번째 적군이 떄림");
                        num2++;
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


