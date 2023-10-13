using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterManagement : MonoBehaviour
{
    [SerializeField]
    List<Button> chapters = new List<Button>();
    

    int btnLevel;


    void Start()
    {
        ChapterButtonDisable();
    }

    void ChapterButtonDisable()
    {
        //챕터 버튼 누름 비활성화(== 이미지)
        for (int i = 0; i < chapters.Count; i++)
        {
            chapters[i].interactable = false;
        }

        //첫번째만 활성화
        chapters[0].interactable = true;
    }

    public void ChapterReSetting(Button chapterObject)
    {
        //초기화 후 생성된 챕터 다시 넣기
        chapters.Clear();

        chapters.Add(chapterObject);
    }



    public void ChapterClear()
    {
        //챕터 클리어 결과 
        btnLevel++;

        chapters[btnLevel].interactable = true;

        DataManager.Instance.PlayerData.unlockedChapters[btnLevel] = true;  

        //DataManager.Instance.LoadGameData();
    }

    public void ChapterUnlock(int chapterNum)
    {
        //게임씬에서 챕터 클리어 성공 화면 호출

        
    }

    public void ChapterDontClear()
    {
        //게임씬에서 챕터 클리어 실패 화면 호출
    }

    void CallSave()
    {
        DataManager.Instance.Save();
    }

}
