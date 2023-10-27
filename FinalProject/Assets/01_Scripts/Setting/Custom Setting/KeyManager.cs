using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction { UP, DOWN, LEFT, RIGHT, KEYCOUNT}

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>();}

public class KeyManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
    void Awake()
    {
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add ((KeyAction)i, defaultKeys[i]);
        }
    }

    int key = -1;

    void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey)
        {
            KeySetting.keys[(KeyAction)key] = keyEvent.keyCode;
            key = -1;
        }
    }

    public void ChangeKey(int num)
    {
        key = num;
    }
}
