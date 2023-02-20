using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IPlayerState
{
    private PlayerController pController;
    private Vector3 localScale; //바라보는방향 전환 변수
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.MOVE;
        Debug.Log($"무브들옴?,{pController.enumState}");
        pController.player.playerAni.SetBool("isWalk", true);
        localScale = pController.player.transform.localScale;
    }
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    }
    public void StateUpdate()
    {
        MoveAndDirection();
    }
    public void StateExit()
    {
        pController.player.playerAni.SetBool("isWalk", false);
    }

    private void MoveAndDirection()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pController.player.transform.Translate(Vector3.right * pController.player.moveSpeed * Time.deltaTime);
            localScale = new Vector3(1, localScale.y, localScale.z);
            pController.player.transform.localScale = localScale;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pController.player.transform.Translate(Vector3.left * pController.player.moveSpeed * Time.deltaTime);
            localScale = new Vector3(-1, localScale.y, localScale.z);
            pController.player.transform.localScale = localScale;
        }
    } //Move
}
