using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StateMachine : MonoBehaviour {

    private Dictionary<Type, BaseState> availableStates;

    public  BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;

    public void SetState(Dictionary<Type, BaseState> states)
    {
        availableStates = states;
    }

    private void FixedUpdate()
    {
        if(CurrentState == null)
        {
            CurrentState = availableStates.Values.First();
        }

        Type nextState = CurrentState?.Tick();

        if(nextState != null && nextState != CurrentState?.GetType())
        {
            SwitchStates(nextState);
        }
    }

    private void SwitchStates(Type nextState)
    {
        CurrentState = availableStates[nextState];
        //OnStateChanged?.Invoke(CurrentState);
    }

}
