using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillB : IPlayerState
{
    private PlayerController pController;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.SKILLB;
        pController.player.playerAni.SetBool("isSkillB", true);
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    } //StateFixedUpdate
    public void StateUpdate()
    {
        ExitSkillB();
    } //StateUpdate
    public void StateExit()
    {
        pController.player.playerAni.SetBool("isSkillB", false);
        /*Do Nothing*/
    } //StateExit

    //SkillA 애니메이션 종료후 다음상태로 전환하는 함수
    private void ExitSkillB()
    {
        //헤드리스상태에서 스킬B로 머리위치로 순간이동하여 런타임애니메이션컨트롤러가 스컬로 바뀔 경우
        //입력받고있던 애니메이션 bool값이 스컬 런타임컨트롤러로 이관되지않아 강제로 상태를 전환시켜
        //스컬 런타임컨트롤러가 유저입력을 받을 수 있게 처리함
        if (pController.player.playerAni.runtimeAnimatorController.name == "Skul")
        {
            IPlayerState nextState;
            if (pController.isGroundRay.hit.collider != null)
            {
                nextState = new PlayerIdle();
            }
            else
            {
                nextState = new PlayerJump();
            }
            pController.pStateMachine.onChangeState?.Invoke(nextState);
        }

        //플레이어의 애니메이션이 스킬B이고 애니메이션이 종료되었을 때 스킬B상태 탈출
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        && pController.player.playerAni.GetCurrentAnimatorStateInfo(0).IsName("SkillB"))
        {
            IPlayerState nextState;
            //플레이어가 땅인 경우 Idle, 공중인 경우 Jump 상태로 전환
            if (pController.isGroundRay.hit.collider != null)
            {
                nextState = new PlayerIdle();
            }
            else
            {
                nextState = new PlayerJump();
            }
            //Debug.Log($"스킬B후 들어갈 다음 상태{nextState}");
            pController.pStateMachine.onChangeState?.Invoke(nextState);
            return;
        }
    } //ExitSkillA
}
