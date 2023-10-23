using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMotion : MonoBehaviour
{

    void AttackMove(RectTransform myTrans, RectTransform enemyID, float duration) // 내 위치, 적 위치, 이동에 걸리는 시간
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
