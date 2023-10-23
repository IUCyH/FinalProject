using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    List<Image> skillNumImages = new List<Image>();

    [SerializeField]
    List<int> skillNum = new List<int>();

    [SerializeField]
    List<bool> skillClick = new List<bool>();

    int skillAddNum;

    public void SaveNum(string objName, string skillName)
    {
          
    }

}
