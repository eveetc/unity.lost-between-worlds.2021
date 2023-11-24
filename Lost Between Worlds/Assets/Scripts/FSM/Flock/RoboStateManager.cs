using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoboStateManager : MonoBehaviour
{
    public RoboState currentState;
    public RoboState lastState;
    void Update()
    {
        RunStateMachine();
    }
    private void RunStateMachine()
    {
        RoboState nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(RoboState nextState)
    {
        currentState = nextState;
        lastState = currentState;
    }
}