using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxMenu : MonoBehaviour
{
    [SerializeField]
    GameObject mailSlotPrefab;
    GridLayout_Vertical gridLayout;
    ObjectPool<RectTransform> mailSlotPool;

    void Start()
    {
        gridLayout = new GridLayout_Vertical
        (
            7,
            (-365f, 295f),
            (550, 100)
        );

        mailSlotPool = new ObjectPool<RectTransform>(12, () =>
        {
            var obj = Instantiate(mailSlotPrefab);
            var rectTrans = obj.GetComponent<RectTransform>();
            rectTrans.SetParent(transform);
            rectTrans.anchoredPosition = Vector2.zero;
            rectTrans.localScale = Vector3.one;
            gridLayout.AddItem(rectTrans);

            return rectTrans;
        });
        
        gridLayout.SetItems();
    }
}
