using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private EnemyAI _enemyAI;
    private float enemyFireRate = 0.25f;
    private float enemyFireRateCounter;
    private Quaternion desiredRotation;
    private float turnSpeed = 0.2f;
    private float agroRange = 8f;

    public AttackState(EnemyAI enemyAI) : base(enemyAI.gameObject)
    {
        _enemyAI = enemyAI;
        enemyFireRateCounter = enemyFireRate;
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
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, turnSpeed);

        if (enemyFireRateCounter <= 0f)
        {
            Debug.Log("Attack");
            _enemyAI.FireProjectile();
            enemyFireRateCounter = enemyFireRate;
        }

        float distance = Vector2.Distance(transform2D, target2D);

        if(distance > agroRange)
        {
            return typeof(WanderState);
        }

        enemyFireRateCounter -= Time.deltaTime;

        return null;
    }
}
