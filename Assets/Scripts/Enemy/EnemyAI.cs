using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyAI : MonoBehaviour {

	public Transform Target { get; private set; }
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public StateMachine StateMachine => GetComponent<StateMachine>();

    private void Awake()
    {
        // Initialize the state machine
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(WanderState), new WanderState(this, whatIsGround, whatIsPlayer)},
            { typeof(ChaseState), new ChaseState(this)}
        };
        GetComponent<StateMachine>().SetState(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void FireProjectile()
    {

    }
}
