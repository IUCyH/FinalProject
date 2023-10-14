using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Key
{
    Left,
    Right,
    Escape,
    Max
}

public static class InputManager
{
    static Dictionary<Key, KeyCode> keys = new Dictionary<Key, KeyCode>();

    static InputManager()
    {
        InitKeys();
    }

    static void InitKeys()
    {
        keys.Add(Key.Escape, KeyCode.Escape);
    }
    
    public static bool GetKey(Key key)
    {
        return Input.GetKey(keys[key]);
    }

    public static bool GetKeyUp(Key key)
    {
        return Input.GetKeyUp(keys[key]);
    }

    public static bool GetKeyDown(Key key)
    {
        return Input.GetKeyDown(keys[key]);
    }
}
