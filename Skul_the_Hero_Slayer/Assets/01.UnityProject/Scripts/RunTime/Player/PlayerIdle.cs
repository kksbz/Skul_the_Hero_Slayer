using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IPlayerState
{
    private PlayerController pController;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.IDLE;
        // Debug.Log($"아이들 들옴? {pController.enumState}");
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
        /*Do Nothing*/
    } //StateExit
}
