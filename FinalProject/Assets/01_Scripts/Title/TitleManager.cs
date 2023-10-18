using System.Collections;
using System.Collections.Generic;
using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class TitleManager : Singleton<TitleManager>
{
    [SerializeField]
    RectTransform titleCanvas;

    public void SetParentToTitleCanvas(RectTransform rectTrans, Vector2 pos)
    {
        rectTrans.SetParent(titleCanvas);
        rectTrans.anchoredPosition = pos;
    }
}
