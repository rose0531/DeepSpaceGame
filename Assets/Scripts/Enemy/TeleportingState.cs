using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeleportingState : BaseState {

    private AI _enemyAI;
    private float teleportTime = 2f;
    private float teleportTimeCounter;
    private Quaternion desiredRotation;

    public TeleportingState(AI enemyAI) : base(enemyAI.gameObject, enemyAI)
    {
        _enemyAI = enemyAI;
        teleportTimeCounter = 0;
    }

    public override Type Tick()
    {
        rb2d.velocity = Vector2.zero;

        if (_enemyAI.Target == null)
            return _enemyAI.statesList[0];

        if (teleportTimeCounter <= 0)
        {
            FindPointNearTarget();
            teleportTimeCounter = teleportTime;
        }
        else
            teleportTimeCounter -= Time.deltaTime;

        desiredRotation = RotateEnemyTowards(_enemyAI.Target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, _enemyAI.settings.TurnSpeed);

        float distance = Vector2.Distance(Transform2D(transform), Transform2D(_enemyAI.Target));

        Debug.DrawRay(transform.position, transform.right * _enemyAI.settings.AgroMaxDistance, Color.blue);

        if (distance > _enemyAI.settings.AgroMaxDistance)
        {
            return _enemyAI.statesList[0];
        }

        return null;
    }

    private void FindPointNearTarget()
    {
        Vector2 randomPointInCircle = UnityEngine.Random.insideUnitCircle * 3f;
        Vector2 pointNearTarget = Transform2D(_enemyAI.Target) + randomPointInCircle;
        
        if (!Physics2D.OverlapCircle(pointNearTarget, 1f))
        {
            transform.position = pointNearTarget;
        }

        desiredRotation = RotateEnemyTowards(_enemyAI.Target.position);
        transform.rotation = desiredRotation;
        _enemyAI.Attack();
    }
}
