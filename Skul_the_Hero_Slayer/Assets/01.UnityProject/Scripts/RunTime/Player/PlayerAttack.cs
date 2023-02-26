using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : IPlayerState
{
    private PlayerController pController;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.ATTACK;
        if (pController.isGroundRay.hit.collider != null)
        {
            //땅에 있으면 기본공격
            pController.player.playerAni.SetBool("isAttackA", true);
        }
        else
        {
            //공중에 있으면 공중공격
            pController.player.playerAni.SetBool("isJumpAttack", true);
        }
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    } //StateFixedUpdate
    public void StateUpdate()
    {
        ComboAttack();
        ExitAttack();
    } //StateUpdate
    public void StateExit()
    {
        pController.player.playerAni.SetBool("isAttackA", false);
        pController.player.playerAni.SetBool("isAttackB", false);
        pController.player.playerAni.SetBool("isJumpAttack", false);
    } //StateExit

    private void ComboAttack()
    {
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f
        && pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                pController.player.playerAni.SetBool("isAttackA", false);
                pController.player.playerAni.SetBool("isAttackB", true);
            }
        }
    } //ComboAttack

    //공중공격을 한 경우 다음 행동 정하는 함수
    private void ExitAttack()
    {
        //공격 애니메이션이 끝나면 들어감
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
        }
    } //UseJumpAttack
}
