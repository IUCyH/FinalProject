using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterScroll : MonoBehaviour
{
    [SerializeField]
    GameObject moveObject;

    [SerializeField]
    float speed;

    float inplaceSize = 1f;

    [SerializeField] float xRange;

    Vector3 lastMousePosition;
    bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseDelta = currentMousePosition - lastMousePosition;
            if (moveObject.transform.position.x < -xRange)
            {
                moveObject.transform.position = new Vector3(-xRange + inplaceSize, moveObject.transform.position.y, moveObject.transform.position.z);
            }
            // 오른쪽 경계를 넘어간 경우
            else if (moveObject.transform.position.x > xRange)
            {
                moveObject.transform.position = new Vector3(xRange - inplaceSize, moveObject.transform.position.y, moveObject.transform.position.z);
            }
            else
            {
                moveObject.transform.Translate(Vector2.right * mouseDelta.x * speed * Time.deltaTime);
            }
            lastMousePosition = currentMousePosition;
        }
    }
}
