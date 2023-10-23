using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ClickWhat : MonoBehaviour
{
    [SerializeField]
    BattleButton battleButton;

    // ��ư�� ������ �� ȣ��� �Լ�
    public void ClickBtn()
    {
        // ��� Ŭ���� ���� ������Ʈ�� �����ͼ� ����
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        battleButton.SaveNum(clickObject.name, clickObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }
}