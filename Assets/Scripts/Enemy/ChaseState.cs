using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState {

    private AI _enemyAI;
    private Quaternion desiredRotation;

    public ChaseState(AI enemyAI) : base(enemyAI.gameObject, enemyAI)
    {
        _enemyAI = enemyAI;
    }

    public override Type Tick()
    {
        if (_enemyAI.Target == null)
            return _enemyAI.statesList[0];

        // Flip sprite depending on which direction it's going.
        CheckSpriteOrientation();

        // Rotate towards the target.
        desiredRotation = RotateEnemyTowards(_enemyAI.Target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, _enemyAI.settings.TurnSpeed);

        // Move towards the target.
        rb2d.velocity = transform.right * _enemyAI.settings.ChaseSpeed;

        float distance = Vector2.Distance(Transform2D(transform), Transform2D(_enemyAI.Target));

        Debug.DrawRay(transform.position, transform.right * _enemyAI.settings.AgroMaxDistance, Color.blue);

        if(distance > _enemyAI.settings.AgroMaxDistance)
        {
            return _enemyAI.statesList[0];
        }

        if (distance <= _enemyAI.settings.AttackDistance)
        {
            return _enemyAI.statesList[2];
        }

        return null;
    }
}
