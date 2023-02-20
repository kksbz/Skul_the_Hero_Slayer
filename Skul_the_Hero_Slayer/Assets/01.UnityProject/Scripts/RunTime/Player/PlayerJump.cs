using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IPlayerState
{
    private PlayerController pController;
    private int jumpCount = 0;
    private float jumpForce = 8f;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.JUMP;
        Debug.Log($"점프 들옴? {pController.enumState}");
        pController.player.playerAni.SetBool("isJump", true);
        jumpCount += 1;
    }
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    }
    public void StateUpdate()
    {
        if(jumpCount < 3)
        {
            pController.player.playerRb.velocity = pController.player.transform.up * jumpForce;
        }
        /*Do Nothing*/
    }
    public void StateExit()
    {
        jumpCount = 0;
        pController.player.playerAni.SetBool("isJump", false);
        /*Do Nothing*/
    }
}
