using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();

    [SerializeField]
    List<bool> isSkill = new List<bool>();

    //������ ���ʴ�� �ο� ++;
    //1 -> 2 -> 3 ���� ���¿��� 1�� ���ٸ� 1 -> 4
    //���� ���� ���� ū��?
    void Sequence(RectTransform buttonTransform)
    {
        for (int i = 0; isSkill.Count < i; i++)
        {
            if (isSkill[i] == false)
            {
                print("Yes");
                Num(i, buttonTransform);
                break;
                //0��°���� �ƴϸ� 0 ��ȣ ����
            }
        }
    }

    void Num(int addNum, RectTransform buttonTransform)
    {
        sequenceImages[addNum].GetComponent<RectTransform>().anchoredPosition = buttonTransform.anchoredPosition;
        sequenceImages[addNum].SetActive(true);
        isSkill[addNum] = true;
    }

    // ��ư�� ������ �� ȣ��� �Լ�
    public void ClickBtn()
    {
        // ��� Ŭ���� ���� ������Ʈ�� �����ͼ� ����
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        Sequence(clickObject.GetComponent<RectTransform>());
    }
}
