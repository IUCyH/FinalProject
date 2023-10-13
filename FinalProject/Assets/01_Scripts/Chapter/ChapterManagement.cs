using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterManagement : MonoBehaviour
{
    [SerializeField]
    List<Button> chapters = new List<Button>();

    PlayerData playerData;
    [SerializeField]
    Data data;

    int btnLevel;

    [Serializable] 
    public class Data
    {
        // 각 챕터의 잠금여부를 저장할 배열
        public bool[] isUnlock = new bool[5];
    }

    void Start()
    {
        ButtonDisable();
    }

    void ButtonDisable()
    {
        //챕터 버튼 누름 비활성화(== 이미지)
        for (int i = 0; i < chapters.Count; i++)
        {
            chapters[i].interactable = false;
        }

        chapters[0].interactable = true;
    }

    public void ChapterClear()
    {
        //챕터 클리어 결과 
        btnLevel++;

        chapters[btnLevel].interactable = true;

        playerData.chapter++;

        //DataManager.Instance.LoadGameData();
    }

    public void ChapterUnlock(int chapterNum)
    {
        //챕터 클리어 성공 화면 호출

        data.isUnlock[chapterNum] = true;
    }

    public void ChapterDontClear()
    {
        //챕터 클리어 실패 화면 호출
    }

    void CallSave()
    {
        DataManager.Instance.Save();
    }

}
