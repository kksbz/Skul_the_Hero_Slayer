using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : IPlayerState
{
    private PlayerController pController;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.SKILL;
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    } //StateFixedUpdate
    public void StateUpdate()
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
            Debug.Log($"공격 후 들어갈 다음 상태{nextState}");
            pController.pStateMachine.onChangeState?.Invoke(nextState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            pController.player.playerAni.SetBool("isSkillA", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            pController.player.playerAni.SetBool("isSkillB", true);
        }
        /*Do Nothing*/
    } //StateUpdate
    public void StateExit()
    {
        pController.player.playerAni.SetBool("isSkillA", false);
        pController.player.playerAni.SetBool("isSkillB", false);
        /*Do Nothing*/
    } //StateExit
}
