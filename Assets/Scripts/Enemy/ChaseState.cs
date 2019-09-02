using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState {

    private EnemyAI _enemyAI;
    private Quaternion desiredRotation;
    private float enemyFireRateCounter;

    public ChaseState(EnemyAI enemyAI) : base(enemyAI.gameObject, enemyAI)
    {
        _enemyAI = enemyAI;
        enemyFireRateCounter = _enemyAI.settings.FireRate;
    }

    public override Type Tick()
    {
        if (_enemyAI.Target == null)
            return typeof(WanderState);

        // Flip sprite depending on which direction it's going.
        CheckSpriteOrientation();

        // Rotate towards the target.
        desiredRotation = RotateEnemyTowards(_enemyAI.Target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, _enemyAI.settings.TurnSpeed);

        // Move towards the target.
        rb2d.velocity = transform.right * _enemyAI.settings.MoveSpeed;

        float distance = Vector2.Distance(Transform2D(transform), Transform2D(_enemyAI.Target));

        Debug.DrawRay(transform.position, transform.right * _enemyAI.settings.AgroMaxDistance, Color.blue);

        if(distance > _enemyAI.settings.AgroMaxDistance)
        {
            return typeof(WanderState);
        }

        if (distance <= _enemyAI.settings.AttackDistance)
        {
            return typeof(AttackState);
        }

        return null;
    }
}
