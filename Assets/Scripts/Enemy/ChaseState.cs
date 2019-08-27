using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState {

    private EnemyAI _enemyAI;
    private Quaternion desiredRotation;

    public ChaseState(EnemyAI enemyAI) : base(enemyAI.gameObject)
    {
        _enemyAI = enemyAI;
    }

    public override Type Tick()
    {
        if (_enemyAI.Target == null)
            return typeof(WanderState);

        // Rotate towards the target.
        Vector2 target2D = new Vector2(_enemyAI.Target.position.x, _enemyAI.Target.position.y);
        Vector2 transform2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = target2D - transform2D;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        desiredRotation = Quaternion.Euler(0f, 0f, rotZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, _enemyAI.settings.TurnSpeed);

        // Move towards the target.
        transform.Translate(Vector2.right * Time.deltaTime * _enemyAI.settings.ChaseSpeed);

        float distance = Vector2.Distance(transform2D, target2D);

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
