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
        //é�� ��ư ���� ��Ȱ��ȭ(== �̹���)
        for (int i = 0; i < chapters.Count; i++)
        {
            chapters[i].interactable = false;
        }

        //ù��°�� Ȱ��ȭ
        chapters[0].interactable = true;
    }

    public void ChapterReSetting(Button chapterObject)
    {
        //�ʱ�ȭ �� ������ é�� �ٽ� �ֱ�
        chapters.Clear();

        chapters.Add(chapterObject);
    }



    public void ChapterClear()
    {
        //é�� Ŭ���� ��� 
        btnLevel++;

        chapters[btnLevel].interactable = true;

        DataManager.Instance.PlayerData.unlockedChapters[btnLevel] = true;  

        //DataManager.Instance.LoadGameData();
    }

    public void ChapterUnlock(int chapterNum)
    {
        //���Ӿ����� é�� Ŭ���� ���� ȭ�� ȣ��

        
    }

    public void ChapterDontClear()
    {
        //���Ӿ����� é�� Ŭ���� ���� ȭ�� ȣ��
    }

    void CallSave()
    {
        DataManager.Instance.Save();
    }

}
