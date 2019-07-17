using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseAndKeyboardInput : ICharacterInput {

    public float Horizontal { get; private set; }
    public bool Attack { get; private set; }
    public bool Jump { get; private set;  }
    public bool HeldJump { get; private set; }

    public void ReadInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Attack = Input.GetButtonDown("Fire1");
        Jump = Input.GetKeyDown(KeyCode.Space);
        if (Jump) Debug.Log("Jump triggered");
        HeldJump = Input.GetKey(KeyCode.Space);
        if (HeldJump) Debug.Log("HeldJump triggered");
    }
}
