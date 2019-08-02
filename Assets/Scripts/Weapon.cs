using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    private Vector2 mousePosToWorld;
    private bool raised = false;
    [SerializeField] private VoidEvent onWeaponFlip;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponentInParent<CharacterController>();
    }

    void FixedUpdate () {
        Vector2 dist = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
        float rotX = 0f;

        if (dist.x < 0f && controller.m_FacingRight)
        {
            controller.Flip();
        }
        else if (dist.x > 0f && !controller.m_FacingRight)
        {
            controller.Flip();
        }else if(dist.x < 0f && !controller.m_FacingRight)
        {
            rotX = 180f;
        }else if(dist.x > 0f && controller.m_FacingRight)
        {
            rotX = 0f;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        transform.Rotate(rotX, 0f, 0f);
    }
}
