using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private EnemyAI _enemyAI;
    private float enemyFireRateCounter;
    private Quaternion desiredRotation;

    public AttackState(EnemyAI enemyAI) : base(enemyAI.gameObject, enemyAI)
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

        if (enemyFireRateCounter <= 0f)
        {
            _enemyAI.FireProjectile();
            enemyFireRateCounter = _enemyAI.settings.FireRate;
        }

        float distance = Vector2.Distance(Transform2D(transform), Transform2D(_enemyAI.Target));

        if(distance > _enemyAI.settings.AttackDistance && distance <= _enemyAI.settings.AgroMaxDistance)
        {
            return typeof(ChaseState);
        }else if(distance > _enemyAI.settings.AgroMaxDistance)
        {
            return typeof(WanderState);
        }

        enemyFireRateCounter -= Time.deltaTime;

        return null;
    }
}
