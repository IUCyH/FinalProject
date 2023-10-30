using AttackType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//##필수
//유닛 코드
//레벨
//공격력
//방어력 
//생명력
//턴당 체력 재생
//보호막 수치

//#종류 전체 증가
//치명타 (치명타 공격)
//방어구 관통력
//생명력 흡수 
//피해 감소량 

//#단일 증가
//속도
//회피 확률
//기본 공격 피해 반사량
//스킬 공격 피해 반사량


//##플레이어 호출 능력치

//#종류 전체 증가
//공격력 증폭 (%) 증가(+)

//#단일 증가
//기본 공격 증폭(%) 증가(+)
//치명타 확률(%) 증가(+)
//치명타 피해(%) 증가(+)
//방어구 관통력(%) 증가(+)
//생명력 흡수(%) 증가(+)
//방어력 증폭(%) 증가(+)
//턴당 체력 재생 증폭(%) 증가(+)
//속도 증폭(%) 증가(+)

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
        Skill //heal도 포함
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
