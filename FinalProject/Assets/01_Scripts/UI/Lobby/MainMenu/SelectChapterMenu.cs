using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChapterMenu : MonoBehaviour
{
    RectTransform thisRectTrans;

    [SerializeField]
    Vector3 maxRightPos;
    [SerializeField]
    Vector3 maxLeftPos;
    Vector3 deltaMousePos;
    Vector3 additionalMoveDir;
    bool canMove;
    bool additionalMove;
    [SerializeField]
    float maxFreeScrollSpeed = 2000f;
    float freeScrollSpeed;
    [SerializeField]
    float scrollSpeed;
    float timer;

    void Awake()
    {
        thisRectTrans = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        thisRectTrans.anchoredPosition = Vector2.zero;
        canMove = false;
        additionalMove = false;
    }

    public void OnPointerDown()
    {
        canMove = true;
        additionalMove = false;
        
        deltaMousePos = Input.mousePosition;
    }

    public void OnPointerUp()
    {
        var currMousePos = Input.mousePosition;
        
        canMove = false;
        additionalMove = true;
        freeScrollSpeed = scrollSpeed / timer;
        additionalMoveDir = (currMousePos - deltaMousePos).normalized;

        freeScrollSpeed = Mathf.Clamp(freeScrollSpeed, freeScrollSpeed, maxFreeScrollSpeed);
    }

    void Update()
    {
        if (canMove)
        {
            timer += Time.deltaTime;
            
            var currMousePos = Input.mousePosition;
            var dir = (currMousePos - deltaMousePos).normalized;
            dir.y = 0f;

            thisRectTrans.Translate(scrollSpeed * Time.deltaTime * dir);

            deltaMousePos = currMousePos;
        }
        
        else if (additionalMove)
        {
            thisRectTrans.Translate(freeScrollSpeed * Time.deltaTime * additionalMoveDir);
            freeScrollSpeed -= 100f;

            if (freeScrollSpeed <= 0f)
            {
                timer = 0f;
                additionalMove = false;
            }
        }

        var clampX = thisRectTrans.anchoredPosition.x;
        if (clampX > maxRightPos.x) clampX = maxRightPos.x;
        if (clampX < maxLeftPos.x) clampX = maxLeftPos.x;

        thisRectTrans.anchoredPosition = new Vector2(clampX, 0f);
    }
}
