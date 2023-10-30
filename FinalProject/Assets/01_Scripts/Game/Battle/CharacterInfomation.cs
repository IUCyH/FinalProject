using AttackType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

//namespace UnitInfomation
//{
//    enum Essential 
//    {
//        UnitCode = 0,
//        Level = 0,
//        ATK = 0,
//        DEF = 0,
//        Health = 0,
//        HealthPerTurn = 0,
//        ShieldValue = 0
//    }

//    enum EntireType
//    {
//        Critical = 0,
//        ArmorPenetration = 0,
//        LifeAbsorb = 0,
//        DamageReduction = 0
//    }

//    enum IndividualType
//    {
//        ATKSpeed = 0,
//        Evasion = 0,
//        BasicATKDamageReflected = 0,
//        SkillATKDamageReflected = 0
//    }
//}

public class CharacterInfomation : MonoBehaviour
{
    #region UnitState
    [Header("Essential")]
    public int UniCode;
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

    [Header("IndividualType")]
    public int ATKSpeed;
    public int Evasion;
    public int BasicATKDamageReflected;
    public int SkillATKDamageReflected;

    #endregion

    public ATKRange atkRange;
    public ATKType atkType;
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
}
