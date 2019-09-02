using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAITeleporting : AI {

    public GameObject projectilePrefab;
    private int numberOfProjectiles = 3;

    protected override void Awake()
    {
        // Initialize the state machine
        base.Awake();
        facingRight = true;
    }

    protected override void InitializeStateMachine()
    {
        states = new Dictionary<Type, BaseState>(){
            { typeof(WanderState), new WanderState(this)},
            { typeof(TeleportingState), new TeleportingState(this)}
            //{ typeof(AttackState), new AttackState(this)}
        };
        GetComponent<StateMachine>().SetState(states);

        statesList = new List<Type>()
        {
            {typeof(WanderState) },
            {typeof(TeleportingState) }
        };
    }

    public override void Attack()
    {
        Quaternion startingAngle = Quaternion.AngleAxis(-30, Vector3.forward);
        Quaternion stepAngle = Quaternion.AngleAxis(30, Vector3.forward);

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Instantiate(projectilePrefab as GameObject, transform.position, transform.rotation * startingAngle);
            startingAngle = startingAngle * stepAngle;
        }
    }
}
