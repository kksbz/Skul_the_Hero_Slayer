using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IPlayerState
{
    private PlayerController pController;
    private int jumpCount = 0; //2단 점프 변수
    private float jumpForce = 7f;
    private Vector3 localScale; //방향전환 변수
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.JUMP;
        Debug.Log($"점프 들옴? {pController.enumState}");
        pController.isGround = false;
        localScale = pController.player.transform.localScale;
        pController.player.playerAni.SetBool("isJump", true);
    } //StateEnter
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    } //StateFixedUpdate
    public void StateUpdate()
    {
        JumpAndMove();
        Jump();
    } //StateUpdate
    public void StateExit()
    {
        jumpCount = 0;
        pController.player.playerAni.SetBool("isJump", false);
    } //StateExit

    //JumpState 유지한채 입력 키 방향으로 이동하면서 바라보는 함수
    private void JumpAndMove()
    {
        //공중에 떠 있으면 시작
        if (pController.isGround == false)
        {
            //입력받은 키 방향으로 이동하면서 바라보는 처리
            if (Input.GetKey(KeyCode.RightArrow))
            {
                pController.transform.Translate(Vector3.right * pController.player.moveSpeed * Time.deltaTime);
                localScale = new Vector3(1, localScale.y, localScale.z);
                pController.player.transform.localScale = localScale;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                pController.transform.Translate(Vector3.left * pController.player.moveSpeed * Time.deltaTime);
                localScale = new Vector3(-1, localScale.y, localScale.z);
                pController.player.transform.localScale = localScale;
            }
        }
    } //JumpAndMove
    
    //점프하는 함수
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C) && jumpCount < 2)
        {
            pController.player.playerRb.velocity = pController.player.transform.up * jumpForce;
            jumpCount += 1;
            Debug.Log(jumpCount);
        }
    } //Jump
}
