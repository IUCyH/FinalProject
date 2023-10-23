using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerExample : MonoBehaviour
{
    public enum State
    {
        start, playerTurn, enemyTurn, win
    }

    public State state;
    public bool isLive; //적 생존 여부

    void Awake()
    {
        state = State.start; //전투 시작알림
        BattleStart();
    }

    void BattleStart()
    {
        //적투 시작 시 캐릭터 등장 애니메이션 등 효과를 넣고싶으면 넣기   

            //플레이어나 적에게 턴 넘기기 (제한시간 고려하기)
        state = State.playerTurn;
    }

    public void PlayerAttackButton()
    {
            //버튼이 계속 눌리는 거를 방지하기 위함
        if (state != State.playerTurn) 
        {
            return;
        }
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("플레이어 공격");
        //공격 스킬, 데미지 등 코드 작성

            //적 죽었으면 전투 종료
        if (!isLive)
        {
            state = State.win;
            EndBattle();
        }
        else
        {
            state = State.enemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    void EndBattle()
    {
        Debug.Log("전투 종료");
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        //적 공격 코드
            //적 공격 끝났으면 플레이어에게 턴 넘기기
        state = State.playerTurn;
    }
}
