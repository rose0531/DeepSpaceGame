using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Keybindings : ScriptableObject {

    public KeyCode jump, attack;

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
}
