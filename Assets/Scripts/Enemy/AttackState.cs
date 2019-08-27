using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private EnemyAI _enemyAI;
    private float enemyFireRateCounter;
    private Quaternion desiredRotation;

    public AttackState(EnemyAI enemyAI) : base(enemyAI.gameObject)
    {
        _enemyAI = enemyAI;
        enemyFireRateCounter = _enemyAI.settings.FireRate;
    }

    public override Type Tick()
    {
        if (_enemyAI.Target == null)
            return typeof(WanderState);

        Vector2 target2D = new Vector2(_enemyAI.Target.position.x, _enemyAI.Target.position.y);
        Vector2 transform2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = target2D - transform2D;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        desiredRotation = Quaternion.Euler(0f, 0f, rotZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, _enemyAI.settings.TurnSpeed);

        if (enemyFireRateCounter <= 0f)
        {
            _enemyAI.FireProjectile();
            enemyFireRateCounter = _enemyAI.settings.FireRate;
        }

        float distance = Vector2.Distance(transform2D, target2D);

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
