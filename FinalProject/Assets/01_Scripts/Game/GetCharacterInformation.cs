using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GetCharacterInformation : MonoBehaviour
{
    // ��ư�� ������ �� ȣ��� �Լ�
    public void ClickCharacter()
    {
        print("ĳ���� Ŭ��");

        // ��� Ŭ���� ���� ������Ʈ�� �����ͼ� ����
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // ��� Ŭ���� ���� ������Ʈ�� �̸��� ��ư �� ���� ���
        print(clickObject.name + ", " + clickObject.GetComponentInChildren<Text>().text);
    }
}