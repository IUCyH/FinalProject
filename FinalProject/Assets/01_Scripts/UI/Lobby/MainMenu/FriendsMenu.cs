using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FriendsMenu : MonoBehaviour, IWindow
{
    [SerializeField]
    GameObject friendProfileBarPrefab;
    GridLayout_Horizontal gridLayout;
    ObjectPool<RectTransform> friendProfileBarPool;

    void Start()
    {
        gridLayout = new GridLayout_Horizontal
        (
            2,
            (300f, -50f),
            (600, 100)
        );
        
        friendProfileBarPool = new ObjectPool<RectTransform>(12, () =>
        {
            var obj = Instantiate(friendProfileBarPrefab);
            var rectTrans = obj.GetComponent<RectTransform>();
            rectTrans.SetParent(transform);
            rectTrans.anchoredPosition = Vector2.zero;
            rectTrans.localScale = Vector3.one;
            
            gridLayout.AddItem(rectTrans);
            return rectTrans;
        });
        
        gridLayout.SetItems();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
