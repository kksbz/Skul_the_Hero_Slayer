using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IPlayerState
{
    private PlayerController pController;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.MOVE;
        Debug.Log($"무브들옴?,{pController.enumState}");
        pController.player.playerAni.SetBool("isWalk", true);
    }
    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    }
    public void StateUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            pController.transform.Translate(Vector3.right * pController.player.moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            pController.transform.Translate(Vector3.left * pController.player.moveSpeed * Time.deltaTime);
        }
    }
    public void StateExit()
    {
        pController.player.playerAni.SetBool("isWalk", false);
    }
}
