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
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
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
            pController.pStateMachine.onChangeState?.Invoke(nextState);
            return;
        }
    } //ExitSkillA
}
