using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton_DontDestroy<WindowManager>
{
    Stack<IWindow> windows = new Stack<IWindow>();

    public void OpenWindowAndPush(IWindow window)
    {
        if (windows.Contains(window)) return;
        
        windows.Push(window);
        window.Open();
    }

    public void CloseWindowAndPop()
    {
        windows.TryPop(out IWindow window);

        if (window != null)
        {
            window.Close();
        }
    }
}
