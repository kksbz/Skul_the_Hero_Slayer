using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    MonsterController mController;
    public IMonsterState currentState
    {
        get;
        private set;
    }

    public StateMachine(IMonsterState defaultState, MonsterController _mController)
    {
        currentState = defaultState;
        mController = _mController;
        SetState(currentState);
    } //StateMachine

    public void SetState(IMonsterState state)
    {
        if (currentState == state)
        {
            Debug.Log("현재 이미 해당 상태입니다");
            return;
        }

        currentState.StateExit();
        currentState = state;
        currentState.StateEnter(mController);
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
