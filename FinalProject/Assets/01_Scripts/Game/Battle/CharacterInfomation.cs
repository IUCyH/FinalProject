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
    enum ATKRange
    {
        Single,
        Multi
    }

    enum ATKType
    {
        Basic,
        Skill //heal�� ����
    }
}

namespace UnitInfomation
{
    enum Essential 
    {
        UnitCode,
        Level,
        ATK,
        DEF,
        Health,
        HealthPerTurn,
        ShieldValue
    }
    
    enum EntireType
    {
        Critical,
        ArmorPenetration,
        LifeAbsorb,
        DamageReduction
    }

    enum IndividualType
    {
        ATKSpeed,
        Evasion,
        BasicATKDamageReflected,
        SkillATKDamageReflected
    }
}

public class CharacterInfomation : MonoBehaviour
{
    public void HurtPlayer(int damage)
    {

    }
}
