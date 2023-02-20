using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : IPlayerState
{
    private PlayerController pController;
    private float dashForce = 10f;
    private float dashTime = 0.3f;
    private int dashCount = 0;
    private float dashCooldown = 1f;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.DASH;
        Debug.Log($"대쉬 들옴? {pController.enumState}");
    }
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    }
    public void StateUpdate()
    {
        /*Do Nothing*/
        if (Input.GetKeyDown(KeyCode.Z) && pController.canDash == true)
        {
            pController.CoroutineDeligate(Dash());
        }
    }
    public void StateExit()
    {
        /*Do Nothing*/
    }
    
    //대쉬하는 코루틴
    private IEnumerator Dash()
    {
        Debug.Log($"대쉬시작");
        pController.canDash = false;
        pController.player.playerAni.SetBool("isDash", true);
        dashCount += 1;
        float originalGravity = pController.player.playerRb.gravityScale;
        pController.player.playerRb.gravityScale = 0f;
        pController.player.playerRb.velocity = new Vector2(pController.player.transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);
        pController.player.playerRb.velocity = Vector2.zero;
        pController.player.playerRb.gravityScale = originalGravity;
        pController.player.playerAni.SetBool("isDash", false);
        //2단 대쉬
        if (dashCount >= 2)
        {
            yield return new WaitForSeconds(dashCooldown);
            dashCount = 0;
            pController.canDash = true;
            yield break;
        }
        pController.canDash = true;
    } //Dash
}
