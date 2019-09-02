using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Vector2 mousePosToWorld;                                // Converted mouse position from the screen to a position in the game world.
    private CharacterController controller;                         // Character controller from the player.
    [SerializeField] private GameObject shootPoint;                 // Point on the weapon to spawn projectile.
    [SerializeField] private float m_FireRate;                      // Weapon fire rate.
    private float waitTime = 0;

    private bool m_FireWeapon = false;

    private void Start()
    {
        // Get character controller off the player.
        controller = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        if(waitTime <= 0)
        {
            waitTime = 0;
            // Check if left mouse button is being held down.
            if (InputManager.instance.MouseButton("LeftMouseButton"))
            {
                m_FireWeapon = true;
                waitTime = m_FireRate;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        } 
    }

    void FixedUpdate () {

        if (m_FireWeapon)
        {
            m_FireWeapon = false;
            FireWeapon();
        }

        // Convert mouse position on screen to a point in the game world, then find the distance between the mouse and player.
        Vector2 dist = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        // Rotation in the Z direction based on the distance between mouse and player.
        float rotZ = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;

        // Set rotation for X to zero for now.
        float rotX = 0f;

        // If the mouse is on the left side of the screen and the player is facing right...
        if (dist.x < 0f && controller.m_FacingRight)
        {
            // ... flip the player.
            controller.Flip();
        }
        // If the mouse is on the right side of the screen and the player is not facing right...
        else if (dist.x > 0f && !controller.m_FacingRight)
        {
            // ... flip the player.
            controller.Flip();
        }

        // If the mouse is on the left and the player is not facing right...
        if (dist.x < 0f && !controller.m_FacingRight)
        {
            // ... set the X rotation of the weapon to 180 degrees.
            rotX = 180f;
        // If the mouse is on the right and the player is facing right...
        }else if(dist.x > 0f && controller.m_FacingRight)
        {
            // ... set the X rotation of the weapon to 0 degrees.
            rotX = 0f;
        }

        // Apply Z rotation so the gun can follow the mouse position.
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        // Apply X rotation depending on where the player and mouse position are.
        transform.Rotate(rotX, 0f, 0f);
    }

    private void FireWeapon()
    {
        Instantiate(Resources.Load("Prefab/Bullet") as GameObject, shootPoint.transform.position, shootPoint.transform.rotation);
    }
}
