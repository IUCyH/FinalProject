using AttackType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Skill //heal�� ����
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
            case ATKType.Skill:
                break;
            default:
                break;
        }
    }

    void FindTarget()
    {
        //DataManager.Instance.GetTarget(this);
    }
}
