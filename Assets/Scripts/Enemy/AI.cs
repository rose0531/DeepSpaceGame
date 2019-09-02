using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AI : MonoBehaviour {
    public Transform Target { get; private set; }
    public Dictionary<Type, BaseState> states;
    public List<Type> statesList;
    public EnemySettings settings;
    public bool facingRight;

    public StateMachine StateMachine => GetComponent<StateMachine>();

    protected abstract void InitializeStateMachine();
    public abstract void Attack();

    protected virtual void Awake()
    {
        // Initialize the state machine
        InitializeStateMachine();
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

}
