using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterManagement : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;

    [SerializeField]
    List<Button> chapters = new List<Button>();
    
    [SerializeField]
    Data data;

    int btnLevel;

    [Serializable] 
    public class Data
    {
        // �� é���� ��ݿ��θ� ������ �迭
        public bool[] isUnlock = new bool[5];
    }

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

        //playerData.chapter++;

        //DataManager.Instance.LoadGameData();
    }

    public void ChapterUnlock(int chapterNum)
    {
        //é�� Ŭ���� ���� ȭ�� ȣ��

        data.isUnlock[chapterNum] = true;
    }

    public void ChapterDontClear()
    {
        //é�� Ŭ���� ���� ȭ�� ȣ��
    }

    void CallSave()
    {
        DataManager.Instance.Save();
    }

}
