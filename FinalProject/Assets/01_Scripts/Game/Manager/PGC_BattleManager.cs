using AttackType;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PGC_BattleManager : MonoBehaviour 
{
    //부모
    [SerializeField]
    Transform p1Team;
    [SerializeField]
    Transform p2Team;


    //입력값
    [SerializeField]
    List<int>  p1InputValue = new List<int>();

    [SerializeField]
    List<int> p2InputValue = new List<int>(); //테스트 1234

    [SerializeField]
    List<float> p1AtkSpeed = new List<float>();

    [SerializeField]
    List<float> p2AtkSpeed = new List<float>();

    [SerializeField]
    List<int> p1SkillCount = new List<int>(); //4명의 공격 횟수 //테스트 0000

    [SerializeField]
    List<int> p1ATKCount = new List<int>(); //4명의 공격 횟수 //테스트 0000

    [SerializeField]
    List<int> p2SkillCount = new List<int>(); //4명의 공격 횟수 //테스트 0000

    [SerializeField]
    List<int> p2ATKCount = new List<int>(); //4명의 공격 횟수 //테스트 0000

    //개체 수 카운트
    [SerializeField]
    int p1TeamCount;
    [SerializeField]
    int p2TeamCount;

    void Update() 
    {
        p1TeamCount = p1Team.childCount;
        p2TeamCount = p2Team.childCount;   
    }

    public void GetInputSeqeunce(List<int> myInputSkillNum)
    {
        p1InputValue = myInputSkillNum;
    } 

    [SerializeField]
    PGC_GameManager gameManager;

    public void GetUnitSpeed()
    {
        var list1 = gameManager._p1UnitList;
        var list2 = gameManager._p2UnitList;

        for (int i = 0; i < list1.Count; i++)
        {
            GameObject p1Obj = list1[i].gameObject;

            var p1Information = p1Obj.GetComponent<PGC_Unit>();

            p1AtkSpeed.Add(p1Information.ATKSpeed);
        }

        for (int i = 0; i < list2.Count; i++)
        {
            GameObject p2Obj = list2[i].gameObject;

            var p2Information = p2Obj.GetComponent<PGC_Unit>();

            p2AtkSpeed.Add(p2Information.ATKSpeed);
        }
    }

    //전투실행, 2배속 고려
    public void FightStart()
    {
        Check();
        StartBasicATK();
        FightEnd();
    }
 
    void FightEnd()
    {
        Debug.Log("FightEnd");
        //p1InputValue.Clear();
        //p2InputValue.Clear();
        //PGC_BattleButton.sequenceValue.Clear();
        //PGC_BattleButton.sequencePlayer.Clear();

        if (p1TeamCount + p2TeamCount < 1)
        {
            //BattleEnd();
        }
        else
        {
            //자동전투 고려 또는 다시 
        }
    }

    bool ArePositionsEqual(List<int> list1, List<int> list2, int valueToCompare) //리스트의 인덱스 같은 값 체크
    {
        int indexInList1 = list1.IndexOf(valueToCompare);
        int indexInList2 = list2.IndexOf(valueToCompare);

        return indexInList1 == indexInList2 && indexInList1 != -1;
    }

    void Check()
    {
        for (int i = 0; i < 4; i++) //수정필요
        {
            bool result = ArePositionsEqual(p1InputValue, p2InputValue, i + 1); //1부터 같은지 체크
       
            if (result) //스킬순서가 같다면 속도 비교 실행
            {
                SameSkillSequence(i); //AtkSpeed는 인풋 순서대로니깐 걱정마세요               
            }
            else //스킬순서가 다르다면 둘 중 랜덤 실행
            {
                RandomSkillSequence(i); //AtkSpeed는 인풋 순서대로니깐 걱정마세요
            }
            //Debug.Log("i의 위치가 두 리스트에서 똑같은가? " + result);
        }

        
    }

    void SameSkillSequence(int order) //스킬 사용 순서가 같으면 속도가 높은 것 실행 후 2번째 것 실행
    {
        if (p1AtkSpeed[order] > p2AtkSpeed[order]) 
        {
            float threshold = p1AtkSpeed[order] * 0.05f;
            if (threshold > p1AtkSpeed[order] - p2AtkSpeed[order]) //동속전
            {
                //동속전 이펙트 실행 포함
                RandomSkillSequence(order); 
            }
            else //그냥 실행
            {
                //print("먼저 때린 유닛 : p1");
                p1SkillTest(order);
                p2SkillTest(order);
            }
        }
        else
        {
            float threshold = p2AtkSpeed[order] * 0.05f;
            if (threshold > p2AtkSpeed[order] - p1AtkSpeed[order]) //동속전
            {
                //동속전 이펙트 실행 포함
                RandomSkillSequence(order);
            }
            else //그냥 실행
            {
                //print("먼저 때린 유닛 : p2");
                p2SkillTest(order);
                p1SkillTest(order);
            }
        }
    }

    void RandomSkillSequence(int order)
    {
        //50% 처리
        bool halfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);
        if (halfATK)
        {
            //print("먼저 때린 유닛 : p1");
            p1SkillTest(order);
            p2SkillTest(order);
        }
        else
        {
            //print("먼저 때린 유닛 : p2");
            p2SkillTest(order);
            p1SkillTest(order);
        }
    }

    void StartBasicATK() //속도 능력치가 높은 순으로 기본공격 실행
    {
        while (p1AtkSpeed.Any() || p2AtkSpeed.Any())
        {
            float maxSpeedP1 = p1AtkSpeed.Any() ? p1AtkSpeed.Max() : float.MinValue;
            float maxSpeedP2 = p2AtkSpeed.Any() ? p2AtkSpeed.Max() : float.MinValue;

            int indexP1 = p1AtkSpeed.IndexOf(maxSpeedP1);
            int indexP2 = p2AtkSpeed.IndexOf(maxSpeedP2);

            if (maxSpeedP1 > maxSpeedP2)
            {
                print("가장 높은 속도: P1" + maxSpeedP1);
                p1ATKTest(indexP1);
                p1AtkSpeed.Remove(maxSpeedP1);
            }
            else if (maxSpeedP2 > maxSpeedP1)
            {
                print("가장 높은 속도: P2" + maxSpeedP2);
                p2ATKTest(indexP2);
                p2AtkSpeed.Remove(maxSpeedP2);
            }
            else
            {
                float maxSpeed = Math.Max(maxSpeedP1, maxSpeedP2);

                if (maxSpeedP1 == maxSpeed && maxSpeedP2 == maxSpeed)
                {
                    //50% 처리
                    bool halfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);
                    if (halfATK)
                    {
                        print("가장 높은 속도: P1" + maxSpeedP1);
                        p1ATKTest(indexP1);
                        p1AtkSpeed.Remove(maxSpeedP1);
                    }
                    else
                    {
                        print("가장 높은 속도: P2" + maxSpeedP2);
                        p2ATKTest(indexP2);
                        p2AtkSpeed.Remove(maxSpeedP2);
                    }
                }
                else if (maxSpeedP1 == maxSpeed)
                {
                    print("가장 높은 속도: P1" + maxSpeedP1);
                    p1ATKTest(indexP1);
                    p1AtkSpeed.Remove(maxSpeedP1);
                }
                else if (maxSpeedP2 == maxSpeed)
                {
                    print("가장 높은 속도: P2" + maxSpeedP2);
                    p2ATKTest(indexP2);
                    p2AtkSpeed.Remove(maxSpeedP2);
                }
            }
        }

        
    }

    ATKType aTKType;

    
    void p1SkillTest(int order)
    {     
        p1SkillCount[order]++;
        var list1 = gameManager._p1UnitList;
        GameObject p1Obj = list1[order].gameObject;
        var p1Unit = p1Obj.GetComponent<PGC_Unit>();

        p1Unit.FindTarget(ATKType.ATKSkill);

    }

    void p2SkillTest(int order)
    {
        p2SkillCount[order]++;

    }

    void p1ATKTest(int order)
    {
        p1ATKCount[order]++;

    }

    void p2ATKTest(int order)
    {
        p2ATKCount[order]++;

    }

}



