using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class ClickWhat : MonoBehaviour
{
    [SerializeField]
    BattleButton battleButton;

    [SerializeField]
    List<int> skillID = new List<int>();


    // 버튼을 눌렀을 때 호출될 함수
    public void ClickBtn()
    {
        // 방금 클릭한 게임 오브젝트를 가져와서 저장
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        battleButton.Sequence(clickObject.GetComponent<RectTransform>());
    }
}
