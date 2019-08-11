using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance;

    public Keybindings keybindings;

    // Singleton - We only want one instance of InputManager.
    private void Awake()
    {
        // If the instance doesn't exist yet, then assign 'this' script to it.
        if(instance == null)
        {
            instance = this;
        }
        // If the instance is not 'this' instance, then delete this instance.
        else if(instance != this)
        {
            Destroy(this);
        }

        // This prevents our InputManager from being destroyed when switching scenes.
        DontDestroyOnLoad(this);
    }

    // Same as GetKeyDown
    public bool KeyDown(string key)
    {
        return Input.GetKeyDown(keybindings.CheckKey(key));
    }

    // Same as GetKey
    public bool Key(string key)
    {
        return Input.GetKey(keybindings.CheckKey(key));
    }

    public bool MouseButton(string key)
    {
        return Input.GetMouseButton(keybindings.CheckMouse(key));
    }
}
