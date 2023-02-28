using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IPlayerState
{
    private PlayerController pController;
    private int jumpCount = 0; //2단 점프 변수
    private float jumpForce = 7f;
    private Vector3 direction; //이동할 방향 변수
    private Vector3 localScale; //방향전환 변수
    private GameObject jumpEffect; //점프이펙트 변수
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.JUMP;
        jumpEffect = pController.gameObject.FindChildObj("JumpEffect");
        if (pController.isGroundRay.hit.collider != null)
        {
            jumpCount = 0;
        }
        localScale = pController.player.transform.localScale;
    } //StateEnter
    public void StateFixedUpdate()
    {
        PlayerFall();
    } //StateFixedUpdate
    public void StateUpdate()
    {
        JumpEffectOff();
        JumpAndMove();
        Jump();
    } //StateUpdate

    public void StateExit()
    {
        pController.player.playerAni.SetBool("isJump", false);
        pController.player.playerAni.SetBool("isFall", false);
    } //StateExit

    //JumpState 유지한채 입력 키 방향으로 이동하면서 바라보는 함수
    private void JumpAndMove()
    {
        //공중에 떠 있으면 시작
        //입력받은 키 방향으로 이동하면서 바라보는 처리
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && pController.isGroundRay.hit.collider == null)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                localScale = new Vector3(1, localScale.y, localScale.z);
                direction = Vector3.right;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                localScale = new Vector3(-1, localScale.y, localScale.z);
                direction = Vector3.left;
            }
            pController.player.transform.localScale = localScale;
            pController.player.transform.Translate(direction * pController.player.moveSpeed * Time.deltaTime);
        }
    } //JumpAndMove

    //플레이어가 낙하를 시작하면 Fall애니로 전환하는 함수
    private void PlayerFall()
    {
        if (pController.player.playerRb.velocity.y < -1)
        {
            pController.player.playerAni.SetBool("isFall", true);
        }
    } //PlayerFall

    //점프하는 함수
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C) && jumpCount < 2)
        {
            if (jumpCount == 1)
            {
                Vector3 playerVelocity = pController.player.playerRb.velocity;
                pController.player.playerRb.velocity = new Vector3(playerVelocity.x, 0, playerVelocity.z);

                jumpEffect.SetActive(true);
            }
            pController.player.playerAni.SetBool("isFall", false);
            pController.player.playerAni.SetBool("isJump", true);
            pController.player.playerAudio.clip = pController.player.jumpSound;
            pController.player.playerAudio.Play();
            pController.player.playerRb.velocity = pController.player.transform.up * jumpForce;
            jumpCount += 1;
        }
    } //Jump

    //점프이펙트 비활성화 하는 함수
    private void JumpEffectOff()
    {
        if (jumpEffect.GetComponentMust<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            jumpEffect.SetActive(false);
        }
    } //JumpEffectOff
}
