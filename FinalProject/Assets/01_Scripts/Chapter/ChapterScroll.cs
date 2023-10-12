using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterScroll : MonoBehaviour
{
    [SerializeField]
    GameObject moveObject;

    Vector2 lastMousePosition;

    [SerializeField]
    float speed;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 mouseDelta = currentMousePosition - lastMousePosition;

            Vector3 newObjPosition = moveObject.transform.position;
            newObjPosition.x -= mouseDelta.x * Time.deltaTime * speed;

            moveObject.transform.position = newObjPosition; 
        }

        lastMousePosition = Input.mousePosition;
    }


}
