using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PStateMachine : MonoBehaviour
{
    PlayerController pController;
    public IPlayerState currentState
    {
        get;
        private set;
    }

    public PStateMachine(IPlayerState defaultState, PlayerController _pController)
    {
        currentState = defaultState;
        Debug.Log(currentState);
        pController = _pController;
        SetState(currentState);
    } //StateMachine

    public void SetState(IPlayerState state)
    {
        if (currentState == state)
        {
            return;
        }

        currentState.StateExit();
        currentState = state;
        currentState.StateEnter(pController);
    } //SetState

    public void DoFixedUpdate()
    {
        currentState.StateFixedUpdate();
    } //DoFixedUpdate

    public void DoUpdate()
    {
        currentState.StateUpdate();
    } //DoUpdate
}
