using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PGC_CheckAbility : MonoBehaviour //CharacterInfomation에서 ID 갖고오면 됨 정보창 띄우기
{
    // 버튼을 눌렀을 때 호출될 함수
    public void ClickCharacter()
    {
        print("캐릭터 클릭");

        // 방금 클릭한 게임 오브젝트를 가져와서 저장
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // 방금 클릭한 게임 오브젝트의 이름과 버튼 속 문자 출력
        print(clickObject.name + ", " + clickObject.GetComponentInChildren<Text>().text);
    }
}
