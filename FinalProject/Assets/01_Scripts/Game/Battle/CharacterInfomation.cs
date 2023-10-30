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
    enum ATKRange
    {
        Single,
        Multi
    }

    enum ATKType
    {
        Basic,
        Skill //heal도 포함
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
