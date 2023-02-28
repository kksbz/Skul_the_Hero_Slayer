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
