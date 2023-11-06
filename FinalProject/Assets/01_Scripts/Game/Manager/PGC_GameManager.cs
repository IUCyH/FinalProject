using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGC_GameManager : Singleton<PGC_GameManager>
{
    public List<RectTransform> _unitPool = new List<RectTransform> ();

    public List<PGC_Unit> _p1UnitList = new List<PGC_Unit>();

    public List<PGC_Unit> _p2UnitList = new List<PGC_Unit>();

    [SerializeField]
    PGC_BattleManager battleManager;

    void Start()
    {
        SetUnitList();
        battleManager.GetUnitSpeed();


    }

    void SetUnitList()
    {
        _p1UnitList.Clear();
        _p2UnitList.Clear();

        for (int i = 0;  i < _unitPool.Count; i++)
        {
            for (int j = 0;  j < _unitPool[i].childCount; j++)
            {
                switch (i)
                {
                    case 0:
                        _p1UnitList.Add(_unitPool[i].GetChild(j).GetComponent<PGC_Unit>());
                        _unitPool[i].GetChild(j).gameObject.tag = "p1";
                        break;
                    case 1:
                        _p2UnitList.Add(_unitPool[i].GetChild(j).GetComponent<PGC_Unit>());
                        _unitPool[i].GetChild(j).gameObject.tag = "p2";
                        break;
                }
            }
        }
    }

    

    public PGC_Unit GetTarget(PGC_Unit unit)
    {
        PGC_Unit tunit = null;

        List<PGC_Unit> tList = new List <PGC_Unit>();
        switch (unit.tag)
        {
            case "p1": tList = _p1UnitList; break;
            case "p2": tList = _p1UnitList; break;
        }

        for (int i = 0; i < tList.Count; i++) //공격할 상대 찾기
        {
            //float tDis = 
        }


        return tunit;
    }
}
