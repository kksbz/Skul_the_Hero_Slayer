using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : IPlayerState
{
    private PlayerController pController;
    private float dashForce = 10f; //대쉬 속도
    private float dashTime = 0.3f; //대쉬 지속 시간
    private int dashCount = 0; //2단 대쉬 변수
    private float dashCooldown = 1f; //대쉬 쿨다운
    private GameObject dashEffect; //대쉬 이펙트
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.DASH;
        dashEffect = pController.gameObject.FindChildObj("DashEffect");
        pController.CoroutineDeligate(Dash());
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    } //StateFixedUpdate
    public void StateUpdate()
    {
        /*Do Nothing*/
    } //StateUpdate
    public void StateExit()
    {
        //대쉬를 점프로 캔슬했을 때 velocity가 zero가되면 바로 낙하 하는걸 방지
        //대쉬 상태를 나갈 때 플레이어의 velocity 초기화
        pController.player.playerRb.velocity = Vector2.zero;
        pController.player.playerAni.SetBool("isDash", false);
        pController.player.playerAni.SetBool("isFallRepict", false);
    } //StateExit

    //대쉬하는 코루틴
    private IEnumerator Dash()
    {
        //플레이어의 Hit상태를 bool값으로 체크해 무적상태 구현
        pController.isHit = true;
        pController.player.playerAudio.clip = pController.player.dashSound;
        pController.player.playerAudio.Play();
        dashEffect.SetActive(true);
        // Debug.Log($"대쉬시작");
        pController.canDash = false;
        pController.player.playerAni.SetBool("isDash", true);
        dashCount += 1;
        //대쉬중 Gravity영향을 받지 않음
        float originalGravity = pController.player.playerRb.gravityScale;
        pController.player.playerRb.gravityScale = 0f;
        pController.player.playerRb.velocity = new Vector2(pController.player.transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);
        //대쉬가 끝나면 Gravity 원래 값으로 되돌림
        pController.player.playerRb.gravityScale = originalGravity;
        pController.player.playerAni.SetBool("isDash", false);
        pController.isHit = false;
        dashEffect.SetActive(false);

        IPlayerState lastState;
        //대쉬가 끝난 후 다음으로 이어질 상태 체크
        if (pController.isGroundRay.hit.collider != null)
        {
            //땅 위에 있으면 Idle
            lastState = new PlayerIdle();
        }
        else
        {
            //공중에 있으면 Jump
            lastState = new PlayerJump();
        }

        //대쉬가 끝나면 강제로 이전 상태로 전환하는 ActionEvent =>대쉬 상태 탈출
        //점프로 대쉬를 캔슬할 경우 Action실행을 하지 않기 위해 예외처리
        if (pController.enumState == PlayerController.PlayerState.DASH)
        {
            pController.pStateMachine.onChangeState?.Invoke(lastState);
        }
        //2단 대쉬
        if (dashCount >= 2)
        {
            yield return new WaitForSeconds(dashCooldown);
            dashCount = 0;
        }
        pController.canDash = true;
    } //Dash
}
