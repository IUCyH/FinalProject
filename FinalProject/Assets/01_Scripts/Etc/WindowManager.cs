using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton_DontDestroy<WindowManager>
{
    Stack<GameObject> windows = new Stack<GameObject>();

    public void OpenWindowAndPush(GameObject window)
    {
        if (windows.Contains(window)) return;
        
        windows.Push(window);
        window.SetActive(true);
    }

    public void CloseWindowAndPop()
    {
        windows.TryPop(out GameObject window);

        if (window != null)
        {
            window.SetActive(false);
        }
    }
}
