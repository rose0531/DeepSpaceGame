using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    private EnemyAI _enemyAI;

    private float turnTimeCounter;
    private Quaternion desiredRotation;
    private Vector2 direction;

    private Quaternion agroStartingAngle = Quaternion.AngleAxis(-60, Vector2.right);
    private Quaternion agroStepAngle = Quaternion.AngleAxis(5, Vector2.right);

    public WanderState(EnemyAI enemyAI) : base(enemyAI.gameObject)
    {
        _enemyAI = enemyAI;
        turnTimeCounter = _enemyAI.settings.TurnTimer;
    }

    public override Type Tick()
    {
        Transform chaseTarget = CheckForAgro();
        if(chaseTarget != null)
        {
            // Set the target for the enemy to the chaseTarget.
            _enemyAI.SetTarget(chaseTarget);

            // Switch enemies state to ChaseState.
            return typeof(ChaseState);
        }

        if (IsForwardBlocked() || turnTimeCounter <= 0)
        {
            FindRandomDestination();
            turnTimeCounter = _enemyAI.settings.TurnTimer;
        }
        else
            turnTimeCounter -= Time.deltaTime;

        // Draw our raycast for debugging.
        Debug.DrawRay(transform.position, transform.right * _enemyAI.settings.WallCheckRaycastDistance, Color.magenta);

        // Apply our desired rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, _enemyAI.settings.TurnSpeed);

        // Move the enemy.
        transform.Translate(Vector2.right * Time.deltaTime * _enemyAI.settings.MoveSpeed);
 
        return null;
    }


    private void FindRandomDestination()
    {
        // Pick a random destination.
        Vector2 randDirection = new Vector2(UnityEngine.Random.Range(-_enemyAI.settings.RandomDirectionRange, _enemyAI.settings.RandomDirectionRange),
                                            UnityEngine.Random.Range(-_enemyAI.settings.RandomDirectionRange, _enemyAI.settings.RandomDirectionRange));
        Vector2 pointInFrontOfEnemy = transform.position + (transform.right * 4f);

        // Random point we want to travel to.
        Vector2 randDestination = pointInFrontOfEnemy + randDirection;

        // Convert transform position (Vector3) to a Vector2.
        Vector2 transform2d = new Vector2(transform.position.x, transform.position.y);

        // Find the direction between the random destination and the enemy.
        direction = randDestination - transform2d;

        // Normalize the direction. (Make it a magnitude of 1f).
        direction.Normalize();

        // Find the rotation of the enemy on the Z-axis to face the rand direction we want to move towards.
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        desiredRotation = Quaternion.Euler(0f, 0f, rotZ - 90);
    }

    private bool IsForwardBlocked()
    {
        return Physics2D.Raycast(transform.position,                    // Enemy position.
                                 transform.right,                       // Direction to fire the raycast.
                                 _enemyAI.settings.WallCheckRaycastDistance,    // Distance to fire the raycast.
                                 _enemyAI.settings.WhatIsGround);               // LayerMask to check for.
    }

    private Transform CheckForAgro()
    {
        Quaternion angle = transform.rotation * agroStartingAngle;
        Vector2 dir = angle * Vector2.right;
        RaycastHit2D hit;

        for (int i = 0; i < _enemyAI.settings.AgroRays; i++)
        {
            hit = Physics2D.Raycast(transform.position, dir, _enemyAI.settings.AgroDistance, _enemyAI.settings.WhatIsPlayer);
            if (hit)
            {
                Debug.DrawRay(transform.position, dir * _enemyAI.settings.AgroDistance, Color.yellow);
                PlayerMovement player = hit.collider.GetComponent<PlayerMovement>();
                if(player != null)
                    return player.transform;
            }
            dir = agroStepAngle * dir;
        }

        return null;
    }
}
