using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;

    public IState CurrentState { get { return currentState; } }
    public void ChangeState(IState nextState) {
        currentState?.Exit();
        currentState = nextState;
        currentState.Initialize();
    }
    public void Execute() {
        currentState?.Execute();
    }
}
