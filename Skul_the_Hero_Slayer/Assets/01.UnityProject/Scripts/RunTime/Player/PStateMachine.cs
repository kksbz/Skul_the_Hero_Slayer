using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PStateMachine : MonoBehaviour
{
    PlayerController pController;
    public Action<IPlayerState> onChangeState; //강제로 빠져나가야 하는 경우 사용될 Action
    public IPlayerState currentState
    {
        get;
        private set;
    }
    //생성자 초기화시 기본상태세팅
    public PStateMachine(IPlayerState defaultState, PlayerController _pController)
    {
        //초기화시 Action에 SetState함수 저장
        onChangeState += SetState;
        currentState = defaultState;
        // Debug.Log(currentState);
        pController = _pController;
        SetState(currentState);
    } //StateMachine

    //입력받은 상태를 체크하여 상태전환하는 함수
    public void SetState(IPlayerState state)
    {
        //입력받은 상태가 입력전 상태와 동일할 경우 리턴
        if (currentState == state)
        {
            return;
        }
        currentState.StateExit();
        currentState = state;
        currentState.StateEnter(pController);
    } //SetState

    //각 상태의 FixedUpdate를 대신 수행시켜줄 함수
    public void DoFixedUpdate()
    {
        currentState.StateFixedUpdate();
    } //DoFixedUpdate

    //각 상태의 Update를 대신 수행시켜줄 함수
    public void DoUpdate()
    {
        currentState.StateUpdate();
    } //DoUpdate
}
