using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChapterMenu : MonoBehaviour, IWindow
{
    [SerializeField]
    RectTransform chaptersRectTrans;

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

    public void OnPointerDown()
    {
        canMove = true;
        additionalMove = false;
        Debug.Log("Pointer Down");
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

            chaptersRectTrans.Translate(scrollSpeed * Time.deltaTime * dir);

            deltaMousePos = currMousePos;
        }
        
        else if (additionalMove)
        {
            chaptersRectTrans.Translate(freeScrollSpeed * Time.deltaTime * additionalMoveDir);
            freeScrollSpeed -= 50f;

            if (freeScrollSpeed <= 0f)
            {
                timer = 0f;
                additionalMove = false;
            }
        }

        var clampX = chaptersRectTrans.anchoredPosition.x;
        if (clampX > maxRightPos.x) clampX = maxRightPos.x;
        if (clampX < maxLeftPos.x) clampX = maxLeftPos.x;

        chaptersRectTrans.anchoredPosition = new Vector2(clampX, 0f);
    }

    public void Open()
    {
        chaptersRectTrans.anchoredPosition = Vector2.zero;
        canMove = false;
        additionalMove = false;
        
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
