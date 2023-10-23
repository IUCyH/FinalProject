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


    // ��ư�� ������ �� ȣ��� �Լ�
    public void ClickBtn()
    {
        // ��� Ŭ���� ���� ������Ʈ�� �����ͼ� ����
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        battleButton.Sequence(clickObject.GetComponent<RectTransform>());
    }
}
