using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires CharacterController script to be attached to the object.
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;

    [Range(0, 100)] public float moveSpeed;     // Move speed of the character.

    private bool jumped = false;      // If player jumped.
    private bool attacked = false;    // If player attacked.
    private float horizontal;         // Holds horizontal move value.

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Read character input in Update().
    private void Update()
    {
        // Get horizontal movement.
        horizontal = Input.GetAxisRaw("Horizontal");

        // Check if jump was pressed.
        if (InputManager.instance.KeyDown("Jump"))
            jumped = true;
    }

    // Apply physics to the character in FixedUpdate().
    private void FixedUpdate()
    {
        // Move character.
        controller.Move(horizontal * moveSpeed * Time.fixedDeltaTime);

        // If character pressed the jump key
        if (jumped)
        {
            jumped = false;
            controller.Jump();
        }
    }
}
