﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody2D Rb { get; set; }                                         // Access to the RigidBody2D of the character.
    public Vector2 m_Velocity = Vector2.zero;                                   // Current velocity of the character.
    public bool m_FacingRight = true;                                           // Bool that keeps track of which direction the character is facing.
    public bool m_Grounded;                                                     // Bool set when character is touching the ground.

    [SerializeField] private float m_JumpForce = 200f;                          // Jump force to be applied to the character.
    [SerializeField] private LayerMask m_WhatIsGround;                          // Layer name that the ground is on.
    [SerializeField] private Transform m_GroundCheck;                           // Transform of where the characters feet are.
    [SerializeField] private float m_GroundCheckRadius;                         // OverlapCircle radius we use to check if our player is touching the ground.
    [SerializeField] private float m_MaxSpeed = 7f;                             // Max speed the character can travel at.
    private float m_MoveSpeedMultiplier = 10f;                                  // Value to multiple the move speed with.
    private float m_MovementSmoothing = 0.05f;                                  // The amount of smoothing to be applied to the character when it moves.

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // Check if character is touching the ground.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundCheckRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            // If the gameObject the character GroundCheck collided with isn't this gameObject then ...
            if (colliders[i].gameObject != gameObject)
            {
                // ... we are grounded.
                m_Grounded = true;
            }
        }

        // Restrict players velocity based on m_MaxSpeed.
        if (Rb.velocity.magnitude > m_MaxSpeed)
            Rb.velocity = Rb.velocity.normalized * m_MaxSpeed;
    }

    public void Move(float move)
    {
        // Move character by finding the target velocity.
        Vector2 targetVelocity = new Vector2(move * m_MoveSpeedMultiplier, Rb.velocity.y);

        // Smooth out the movement before applying velocity to the character.
        Rb.velocity = Vector2.SmoothDamp(Rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    public void Jump()
    {
        // If character is grounded ...
        if (m_Grounded)
        {
            // ... then apply a vertical force to the character.
            m_Grounded = false;
            Rb.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public void Flip()
    {
        // Flip which way the player is facing.
        m_FacingRight = !m_FacingRight;

        // Rotate y axis by 180 to flip the player transform.
        transform.Rotate(0f, 180f, 0f);
    }


}
