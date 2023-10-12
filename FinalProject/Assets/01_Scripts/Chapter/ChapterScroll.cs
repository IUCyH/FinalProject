using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterScroll : MonoBehaviour
{
    Camera m_camera;

    Vector2 lastMousePosition;

    void Start()
    {
        m_camera = GetComponent<Camera>();  
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log(m_camera.transform.position);
            Debug.Log("yes");
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 mouseDelta = currentMousePosition - lastMousePosition;
            Vector2 newObjPosition = m_camera.transform.position;

            newObjPosition.x -= mouseDelta.x * Time.deltaTime;
            Vector3 cameraPosition = new Vector3(newObjPosition.x, newObjPosition.y, -10);

            m_camera.transform.position = cameraPosition;
        }

        lastMousePosition = Input.mousePosition;
    }

}
