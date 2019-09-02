using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySettings : ScriptableObject {


    public float TurnSpeed = 0.2f;                      // Turn speed for the enemy to face the target.
    [SerializeField] float MaxTurnTimer;
    [SerializeField] float MinTurnTimer;

    public float RandomDirectionRange = 4.5f;           // Range for coordinate we pick to go in a random direction. (ex. Range(-4.5, 4.5))
    public float MoveSpeed = 3f;                        // Enemy move speed.
    public float ChaseSpeed = 5f;                       // Enemy chase speed.
    
    public float WallCheckRaycastDistance = 1.5f;       // Raycast distance to check for walls.

    public int AgroRays = 24;                           // Number of rays to cast in front of the enemy in a fan shape to check for agro.
                                                        // A.K.A, field of view for the enemy.

    public float AgroDistance = 6f;                     // Raycast distance to check for agro.
    public float AgroMaxDistance = 11f;                 // Max agro distance the player has to escape for the enemy to stop agroing.

    public float AttackDistance = 3f;                   // Range from which the enemy can attack.

    public float FireRate = 0.5f;                       // Fire rate for the enemy projectile.

    public LayerMask WhatIsGround;                      // Layermask for the ground.
    public LayerMask WhatIsPlayer;                      // Layermask for the player.

    public float GetRandomTurnTimer()
    {
        return Random.Range(MinTurnTimer, MaxTurnTimer);
    }
}
