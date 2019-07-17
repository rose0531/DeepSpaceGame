using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// PController updates player movement.

public class PlayerController : CharacterController {
    [SerializeField] private CharacterStatsEvent onJetpackEquipped;
    

    public override void Awake()
    {
        base.Awake();
        input = new MouseAndKeyboardInput();        // Use mouse and keyboard input type for Player movement.
    }

    private void Update()
    {
        base.UpdateCharacter();
    }
}
