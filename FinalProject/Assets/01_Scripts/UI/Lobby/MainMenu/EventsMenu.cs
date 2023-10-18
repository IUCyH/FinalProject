using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsMenu : MonoBehaviour, IWindow
{
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
