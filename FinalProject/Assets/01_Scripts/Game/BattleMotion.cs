using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMotion : MonoBehaviour
{

    void AttackMove(RectTransform myTrans, RectTransform enemyID, float duration) // �� ��ġ, �� ��ġ, �̵��� �ɸ��� �ð�
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
