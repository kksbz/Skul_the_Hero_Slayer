using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillA : IPlayerState
{
    private PlayerController pController;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.SKILLA;
        pController.player.playerAni.SetBool("isSkillA", true);
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    } //StateFixedUpdate
    public void StateUpdate()
    {
        ExitSkillA();
    } //StateUpdate
    public void StateExit()
    {
        pController.player.playerAni.SetBool("isSkillA", false);
        /*Do Nothing*/
    } //StateExit

    //SkillA 애니메이션 종료후 다음상태로 전환하는 함수
    private void ExitSkillA()
    {
        //스컬상태에서 스킬A로 머리를 날려 런타임애니메이션컨트롤러가 헤드리스로 바뀔 경우
        //입력받고있던 애니메이션 bool값이 헤드리스 런타임컨트롤러로 이관되지않아 강제로 상태를 전환시켜
        //헤드리스 런타임컨트롤러가 유저입력을 받을 수 있게 처리함
        if (pController.player.playerAni.runtimeAnimatorController.name == "SkulHeadless")
        {
            // Debug.Log($"스컬 헤드리스애니 체인지? {pController.player.playerAni.runtimeAnimatorController.name}");
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

        //플레이어의 애니메이션이 스킬A이고 애니메이션이 종료되었을 때 스킬A상태 탈출
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
        && pController.player.playerAni.GetCurrentAnimatorStateInfo(0).IsName("SkillA"))
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
            //Debug.Log($"스킬A후 들어갈 다음 상태{nextState}");
            pController.pStateMachine.onChangeState?.Invoke(nextState);
            return;
        }
    } //ExitSkillA
}
