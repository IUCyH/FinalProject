using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UnitState
{
    idle,
    run,
    attack,
    stun,
    skill,
    death
}

public class PGC_UnitMotion : MonoBehaviour
{

    public UnitState _unitState = UnitState.idle;

    void CheckState()
    {
        switch (_unitState)
        {
            case UnitState.idle:
                break;
            case UnitState.run:
                break;
            case UnitState.attack:
                break;
            case UnitState.stun:
                break;
            case UnitState.skill:
                break;
        }
    }



    


    void NearAttackMove(RectTransform myTrans, RectTransform enemyID, float duration) // 내 위치, 적 위치, 이동에 걸리는 시간
    {
        float t = 0; 

        Vector2 startPos = myTrans.anchoredPosition;
        Vector2 endPos = enemyID.anchoredPosition;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            myTrans.anchoredPosition = Vector2.Lerp(startPos, endPos, t);          
        }
    }


}
