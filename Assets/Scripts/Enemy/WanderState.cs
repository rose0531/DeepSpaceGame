using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    private EnemyAI _enemyAI;
    private float turnTime = 8f;
    private float turnTimeCounter;
    private float randomDirectionRadius = 4.5f;
    private float enemySpeed = 3f;
    private Quaternion desiredRotation; 
    private float turnSpeed = 0.2f;
    private Vector2 direction;
    private float raycastDistance = 1.5f;
    private LayerMask whatIsGround;
    private LayerMask whatIsPlayer;
    private int agroCheckRays = 24;
    private float agroRadius = 5f;

    private Quaternion agroStartingAngle = Quaternion.AngleAxis(-60, Vector2.up);
    private Quaternion agroStepAngle = Quaternion.AngleAxis(5, Vector2.up);

    public WanderState(EnemyAI enemyAI, LayerMask ground, LayerMask player) : base(enemyAI.gameObject)
    {
        _enemyAI = enemyAI;
        whatIsGround = ground;
        whatIsPlayer = player;
        turnTimeCounter = turnTime;
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
            turnTimeCounter = turnTime;
        }
        else
            turnTimeCounter -= Time.deltaTime;

        // Draw our raycast for debugging.
        Debug.DrawRay(transform.position, transform.right * raycastDistance, Color.magenta);

        // Apply our desired rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, turnSpeed);

        // Move the enemy.
        transform.Translate(Vector2.right * Time.deltaTime * enemySpeed);
 
        return null;
    }


    private void FindRandomDestination()
    {
        // Pick a random destination.
        Vector2 randDirection = new Vector2(UnityEngine.Random.Range(-randomDirectionRadius, randomDirectionRadius),
                                            UnityEngine.Random.Range(-randomDirectionRadius, randomDirectionRadius));
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
        return Physics2D.Raycast(transform.position, transform.right * raycastDistance, raycastDistance, whatIsGround);
    }

    private Transform CheckForAgro()
    {
        Quaternion angle = transform.rotation * agroStartingAngle;
        Vector2 dir = angle * Vector2.right;
        RaycastHit2D hit;

        for (int i = 0; i < agroCheckRays; i++)
        {
            hit = Physics2D.Raycast(transform.position, dir, agroRadius, whatIsPlayer);
            if (hit)
            {
                Debug.Log("hit: " + hit.collider.name);
                PlayerMovement player = hit.collider.GetComponent<PlayerMovement>();
                if(player != null)
                {
                    Debug.Log("hit player");
                    return player.transform;
                }
                else
                {
                    Debug.DrawRay(transform.position, dir * agroRadius, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, dir * agroRadius, Color.white);
            }
            dir = agroStepAngle * dir;
        }

        return null;
    }
    /*
    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggro()
    {
        float aggroRadius = 5f;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, aggroRadius))
            {
                var drone = hit.collider.GetComponent<Drone>();
                if (drone != null && drone.Team != gameObject.GetComponent<Drone>().Team)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return drone.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * aggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
    */
}
