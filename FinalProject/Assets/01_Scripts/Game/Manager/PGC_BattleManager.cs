using System;
using System.Collections;
using System.Collections.Generic;
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
    List<int> p2InputValue = new List<int>();

    [SerializeField]
    List<int> p1AtkSpeed = new List<int>();

    [SerializeField]
    List<int> p2AtkSpeed = new List<int>();

    [SerializeField]
    List<int> p1AtkCount = new List<int>(); //4명의 공격 횟수

    [SerializeField]
    List<int> p2AtkCount = new List<int>(); //4명의 공격 횟수

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
        //SpeedCheck();
        //SkillSeqencePlay();

        Check();
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

    bool ArePositionsEqual(List<int> list1, List<int> list2, int valueToCompare)
    {
        int indexInList1 = list1.IndexOf(valueToCompare);
        int indexInList2 = list2.IndexOf(valueToCompare);

        return indexInList1 == indexInList2 && indexInList1 != -1;
    }

    void Check()
    {
        for (int i = 0; i < 4; i++) //수정필요
        {
            bool result = ArePositionsEqual(p1InputValue, p2InputValue, i); 
       
            if (result) //스킬순서가 같다면 실행
            {
                SameSkillSequence(i);               
            }
            else //스킬순서가 다르다면 실행
            {
                AnotherSkillSequence(i);
            }
        }
        //Debug.Log("i의 위치가 두 리스트에서 똑같은가? " + result); 
    }

    void SameSkillSequence(int order) //스킬 사용 순서가 같으면 속도가 높은 것 실행 후 2번째 것 실행
    {
        if (p1AtkSpeed[order] > p2AtkSpeed[order]) 
        {
            print("먼저 때린 유닛 : p1");
            p1AtkCount[order]++;
            ATKTest(order);
            p2AtkCount[order]++;
            ATKTest(order);
        }
        else
        {
            print("먼저 때린 유닛 : p2");
            p2AtkCount[order]++;
            ATKTest(order);
            p1AtkCount[order]++;
            ATKTest(order);
        }
    }

    void AnotherSkillSequence(int order) //스킬 사용 순서가 다르면 둘 중 랜덤 실행 
    {
        // 동일한 경우 50% 처리
        bool halfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);
        if (halfATK)
        {
            print("먼저 때린 유닛 : p1");
            p1AtkCount[order]++;
            ATKTest(order);
            p2AtkCount[order]++;
            ATKTest(order);
        }
        else
        {
            print("먼저 때린 유닛 : p2");
            p2AtkCount[order]++;
            ATKTest(order);
            p1AtkCount[order]++;
            ATKTest(order);
        }      
    }

    

    //int testNum = 20;

    //연속공격 고려
    //public void SkillSeqencePlay() 
    //{      

    //    for (int i = 0; i < testNum; i++) //
    //    {
    //        bool isFasterThanP2 = p1AtkSpeed[i] > p2AtkSpeed[i];

    //        if (isFasterThanP2)
    //        {
    //            print("isFasterThanEnemy : true");
    //            int num1 = 1;
    //            if(p1AtkSpeed[i] < p2AtkSpeed[i] * 2)
    //            {
    //                Debug.Log(i + "번째 적군이 떄림");
    //                i++;
    //                continue;
    //            }

    //            for (int j = 0; j < Mathf.Abs(p2AtkSpeed[i] - (p1AtkSpeed[i] * num1)); j++)
    //            {
    //                Debug.Log(i + "번째 아군이 떄림");
    //                num1++;
    //            }

    //        }
    //        else if (isFasterThanP2 && p1AtkSpeed[i] < p2AtkSpeed[i] * 2)
    //        {
    //            print("isFasterThanEnemy : else if");
    //            Debug.Log(i + "번째 적군이 떄림");
    //        }
    //        else
    //        {
    //            print("isFasterThanEnemy : false"); 
    //            if (p2AtkSpeed[i] > p1AtkSpeed[i] * 2)
    //            {
    //                float num2 = 1.5f;

    //                for (int j = 0; j < Mathf.Abs(p1AtkSpeed[i] - (p2AtkSpeed[i] * num2)); j++) 
    //                { 
    //                    Debug.Log(i + "번째 적군이 떄림");
    //                }
    //            }
    //            else
    //            {
    //                Debug.Log(i + "번째 아군이 떄림");
    //            }
    //        }

    //        i++;
    //    }


    //    FightEnd();
    //}

    //때릴 떄 공식, 수식 계산하기
    void ATKTest(int whatP)
    {
        //print("먼저 때린 유닛 :" + whatP);
        
    }


}



