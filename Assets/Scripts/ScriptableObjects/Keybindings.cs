using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Keybindings : ScriptableObject {

    public KeyCode jump, attack;
    public int shoot;

    public KeyCode CheckKey(string key)
    {
        switch(key)
        {
            case "Jump":
                return jump;

            case "Attack":
                return attack;

            default:
                return KeyCode.None;
        }
    }

    public int CheckMouse(string button)
    {
        switch (button)
        {
            case "LeftMouseButton":
                return 0;

            case "RightMouseButton":
                return 1;

            default:
                return -1;
        }
    }
}
