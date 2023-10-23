using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleButton : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sequenceImages = new List<GameObject>();

    [SerializeField]
    List<bool> isSkill = new List<bool>();

    //순서를 차례대로 부여 ++;
    //1 -> 2 -> 3 선택 상태에서 1을 뺐다면 1 -> 4
    //들어온 수가 가장 큰가?
    void Sequence(RectTransform buttonTransform)
    {
        for (int i = 0; isSkill.Count < i; i++)
        {
            if (isSkill[i] == false)
            {
                print("Yes");
                Num(i, buttonTransform);
                break;
                //0번째부터 아니면 0 번호 대입
            }
        }
    }

    void Num(int addNum, RectTransform buttonTransform)
    {
        sequenceImages[addNum].GetComponent<RectTransform>().anchoredPosition = buttonTransform.anchoredPosition;
        sequenceImages[addNum].SetActive(true);
        isSkill[addNum] = true;
    }

    // 버튼을 눌렀을 때 호출될 함수
    public void ClickBtn()
    {
        // 방금 클릭한 게임 오브젝트를 가져와서 저장
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        Sequence(clickObject.GetComponent<RectTransform>());
    }
}
