using AttackType;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

#region Set List
//##�ʼ�
//���� �ڵ�
//����
//���ݷ�
//���� 
//�����
//�ϴ� ü�� ���
//��ȣ�� ��ġ

//#���� ��ü ����
//ġ��Ÿ (ġ��Ÿ ����)
//�� �����
//����� ��� 
//���� ���ҷ� 

//#���� ����
//�ӵ�
//ȸ�� Ȯ��
//�⺻ ���� ���� �ݻ緮
//��ų ���� ���� �ݻ緮


//##�÷��̾� ȣ�� �ɷ�ġ

//#���� ��ü ����
//���ݷ� ���� (%) ����(+)

//#���� ����
//�⺻ ���� ����(%) ����(+)
//ġ��Ÿ Ȯ��(%) ����(+)
//ġ��Ÿ ����(%) ����(+)
//�� �����(%) ����(+)
//����� ���(%) ����(+)
//���� ����(%) ����(+)
//�ϴ� ü�� ��� ����(%) ����(+)
//�ӵ� ����(%) ����(+)
#endregion

namespace AttackType
{
    public enum ATKRange
    {
        Single,
        Multi
    }

    public enum ATKType
    {
        Basic,
        ATKSkill,
        Heal
    }
}

public class PGC_Unit : MonoBehaviour
{
    #region UnitState
    [Header("Arrangement")]
    public int arrangementNumber;

    [Header("Essential")]
    public int UnitCode;
    public int Level;
    public int ATK;
    public int Def;
    public int Health;
    public int HealthPerTurn;
    public int ShieldValue;

    [Header("EntireType")]
    public int Critical;
    public int ArmorPenetration;
    public int LifeAbsorb;
    public int DamageReduction;

    [Header("IndividualType ")]
    public float ATKSpeed;
    public int Evasion;
    public int BasicATKDamageReflected;
    public int SkillATKDamageReflected;

    [Header("AttackType ")]
    public ATKRange atkRange;
    public ATKType atkType;

    #endregion

    public void CheckState()
    {
        switch (atkRange)
        {
            case ATKRange.Single: 
                break;
            case ATKRange.Multi: 
                break;
            default:
                break;
        }

        switch (atkType)
        {
            case ATKType.Basic:
                break;
            case ATKType.ATKSkill:
                break;
            case ATKType.Heal:
                break;
            default:
                break;
        }
    }

    int tagetNum;
    bool isMove;
    [SerializeField]
    Transform myTrans;
    [SerializeField]
    Transform target;

    void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.Lerp(myTrans.position, target.transform.position, 0.05f);
        }
    }

    void FrontCheck(List<PGC_Unit> pList)
    {     
        bool ishalfATK = Dods_ChanceMaker.GetThisChanceResult_Percentage(50);     
        if (pList[0].gameObject.activeSelf || pList[1].gameObject.activeSelf)
        {
            if (ishalfATK)
            {
                tagetNum = 0;
            }
            else
            {
                tagetNum = 1;
            }
        }
        else if (pList[2].gameObject.activeSelf || pList[3].gameObject.activeSelf)
        {
            if (ishalfATK)
            {
                tagetNum = 2;
            }
            else
            {
                tagetNum = 3;
            }
        }

    }


    //���� �� ��� üũ �� ����, ���� ����ϱ�
    public void P1FindTarget(ATKType aTKType)
    {
        FrontCheck(PGC_GameManager.Instance._p2UnitList);
        StartCoroutine(ATKMoveTest(PGC_GameManager.Instance._p2UnitList[tagetNum].gameObject.transform));
    }

    public void P2FindTarget(ATKType aTKType)
    {
        FrontCheck(PGC_GameManager.Instance._p1UnitList);
        StartCoroutine(ATKMoveTest(PGC_GameManager.Instance._p1UnitList[tagetNum].gameObject.transform));
    }

    IEnumerator ATKMoveTest(Transform target1)
    {
        Debug.Log("ATKMoveTest");

        myTrans = GetComponent<Transform>();
        target = target1;

        var mypos = myTrans.position;
        isMove = true;

        yield return new WaitForSeconds(3f);
        target.position = myTrans.position;
        yield return new WaitForSeconds(3f);
        myTrans.position = mypos;

        isMove = false;
    }

    void IsDamaged()
    {

    }

    void IsDead()
    {

    }
}
