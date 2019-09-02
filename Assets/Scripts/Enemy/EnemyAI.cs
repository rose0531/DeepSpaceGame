using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyAI : AI{

    public GameObject projectilePrefab;

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
            { typeof(ChaseState), new ChaseState(this)},
            { typeof(AttackState), new AttackState(this)}
        };
        GetComponent<StateMachine>().SetState(states);

        statesList = new List<Type>()
        {
            {typeof(WanderState) },
            {typeof(ChaseState) },
            {typeof(AttackState) }
        };
    }

    public override void Attack()
    {
        Instantiate(projectilePrefab as GameObject, transform.position, transform.rotation);
    }
}
